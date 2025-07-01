using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using MultiAgent.WebApp.Models;

namespace MultiAgent.WebApp.Services;

public class AppService(IHttpClientFactory httpClientFactory, IServiceScopeFactory scopeFactory)
{
    public async Task<Agent> CreateAgentAsync(CreateAgentRequest request)
    {
        using var scope = scopeFactory.CreateScope();
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync("/agents", request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Agent>()
               ?? throw new InvalidOperationException("Failed to deserialize agent response.");
    }

    public async Task<Models.Thread> CreateThreadAsync()
    {
        using var scope = scopeFactory.CreateScope();
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.PostAsync("/threads", null);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Models.Thread>()
               ?? throw new InvalidOperationException("Failed to deserialize thread response.");
    }

    public async Task<List<Message>> ListMessagesAsync(string threadId)
    {
        using var scope = scopeFactory.CreateScope();
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.GetFromJsonAsync<List<Message>>($"/threads/{threadId}/messages");
        return response ?? new List<Message>();
    }

    public async Task<Message> CreateRunAsync(CreateRunRequest request)
    {
        using var scope = scopeFactory.CreateScope();
        var client = httpClientFactory.CreateClient("ApiClient");
        var response = await client.PostAsJsonAsync("/run", request);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Message>()
               ?? throw new InvalidOperationException("Failed to deserialize run response.");
    }
}