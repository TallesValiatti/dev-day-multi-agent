using System.Text;
using Azure;
using Azure.AI.Agents.Persistent;
using Azure.AI.Projects;
using Azure.Identity;
using MultiAgent.Api.Services.Models;

namespace MultiAgent.Api.Services;

using Thread = Models.Thread;

public class AgentService(IConfiguration configuration)
{
    private const string Instructions = 
        """
        You are an orchestrator agent responsible for managing product-related requests. You do not directly access product or stock information. Instead, you coordinate responses by delegating to two specialized agents:
        
        -> Connected agents:
        Product Info Agent – Retrieves accurate and up-to-date product details.
        Stock Agent – Retrieves current stock quantities.
        
        -> Your responsibilities:
        Understand each request and determine whether it involves product information, stock quantity, or both.
        Call the appropriate agent(s) to fulfill the request.
        Combine and return a unified response based only on the data retrieved from these agents.
        Never fabricate or infer data — always rely on the agents' outputs.
        Ensure responses are clear, complete, and based strictly on the latest API data.
        
        -> Example scenarios you must handle:
        A user requests the price and availability of a product → call both agents.
        A user asks only for stock information → call only the Stock Agent.
        A user wants product specifications → call only the Product Info Agent.
        """;
    
    private PersistentAgentsClient CreateAgentsClient()
    {
        var connectionString = configuration["AiServiceProjectConnectionString"]!;
        return new PersistentAgentsClient(connectionString, new DefaultAzureCredential());
    }
    
    private AIProjectClient CreateProjectClient()
    {
        var connectionString = configuration["AiServiceProjectConnectionString"]!;
        return new AIProjectClient(new Uri(connectionString), new DefaultAzureCredential());
    }

    public async Task<Agent> CreateAgentAsync(CreateAgentRequest request)
    {
        var aiModel = configuration["AiModel"]!;
        var client = CreateAgentsClient();

        PersistentAgent stockAgent = await client.Administration.GetAgentAsync(configuration["Agents:StockAgentId"]!);
        PersistentAgent productAgent = await client.Administration.GetAgentAsync(configuration["Agents:ProductAgentId"]!);
        
        var connectedAgentDefinition = new ConnectedAgentToolDefinition(new ConnectedAgentDetails(stockAgent.Id, stockAgent.Name, "Gets stock information for products."));
        var productAgentDefinition = new ConnectedAgentToolDefinition(new ConnectedAgentDetails(productAgent.Id, productAgent.Name, "Gets product information."));
        
        var agentResponse = await client.Administration.CreateAgentAsync(
            model: aiModel,
            name: "Orchestrator agent",
            instructions: Instructions,
            tools: [connectedAgentDefinition, productAgentDefinition]);

        return new Agent(
            agentResponse.Value.Id,
            agentResponse.Value.Name,
            agentResponse.Value.Instructions);
    }

    public async Task<Thread> CreateThreadAsync()
    {
        var client = CreateAgentsClient();

        var threadResponse = await client.Threads.CreateThreadAsync();

        return new Thread(threadResponse.Value.Id);
    }

    public async Task<Message> CreateRunAsync(CreateRunRequest request)
    {
        var client = CreateAgentsClient();

        await client.Messages.CreateMessageAsync(
            request.ThreadId,
            MessageRole.User,
            request.Message);

        Response<ThreadRun> runResponse = await client.Runs.CreateRunAsync(
            request.ThreadId,
            request.AgentId,
            additionalInstructions: "");

        do
        {
            await Task.Delay(TimeSpan.FromMilliseconds(500));
            runResponse = await client.Runs.GetRunAsync(request.ThreadId, runResponse.Value.Id);
            
        } while (runResponse.Value.Status == RunStatus.Queued || 
                 runResponse.Value.Status == RunStatus.InProgress ||
                 runResponse.Value.Status == RunStatus.RequiresAction);

        if (runResponse.Value.Status == RunStatus.Failed)
        {
            return new Message(
                Guid.NewGuid().ToString(), 
                MessageRole.User.ToString(),
                $"Error: {runResponse.Value.LastError.Message}");
        }
        
        Pageable<PersistentThreadMessage> messages = client.Messages.GetMessages(
            request.ThreadId,
            order: ListSortOrder.Descending);
        
        var message = messages.FirstOrDefault();

        if (message is null)
        {
            throw new Exception("No messages found after run.");
        }
        
        StringBuilder text = new();

        foreach (var contentItem in message.ContentItems)
        {
            if (contentItem is MessageTextContent textItem)
            {
                var annotations = textItem.Annotations;

                if (annotations.Any())
                {
                    var formattedText = textItem.Text;
                    
                    foreach (var annotation in annotations)
                    {
                        if (annotation is MessageTextFileCitationAnnotation messageTextFileCitationAnnotation)
                        {
                            formattedText = formattedText.Replace(messageTextFileCitationAnnotation.Text, $" ({messageTextFileCitationAnnotation.FileId})");
                        }
                        else if (annotation is MessageTextUriCitationAnnotation messageTextUriCitationAnnotation)
                        {
                            formattedText = formattedText.Replace(messageTextUriCitationAnnotation.Text, $" ({messageTextUriCitationAnnotation.UriCitation.Title})");
                        }
                    }
                    text.AppendLine(formattedText);
                }
                else
                {
                    text.AppendLine(textItem.Text);
                }
            }
        }

        if (message.ContentItems.Count == 1 && text.Length > 0)
        {
            text.Length--;
        }

        return new Message(message.Id, message.Role.ToString(), text.ToString());
    }

    public async Task<IEnumerable<Message>> ListMessagesAsync(string threadId)
    {
        var client = CreateAgentsClient();

        Pageable<PersistentThreadMessage> messages = client.Messages.GetMessages(threadId);

        return messages.Select(message =>
        {
            StringBuilder text = new();

            foreach (var contentItem in message.ContentItems)
            {
                if (contentItem is MessageTextContent textItem)
                {
                    text.AppendLine(textItem.Text);
                }
            }

            if (message.ContentItems.Count == 1 && text.Length > 0)
            {
                text.Length--;
            }

            return new Message(message.Id, message.Role.ToString(), text.ToString());
        });
    }
}