namespace MultiAgent.WebApp.Models;

public class CreateAgentRequest
{
    public string Name { get; set; } = null!;
    public string Instructions { get; set; } = null!;
}

public class CreateRunRequest
{
    public string AgentId { get; set; } = null!;
    public string ThreadId { get; set; } = null!;
    public string Message { get; set; } = null!;
}

public class Message
{
    public string Id { get; set; } = null!;
    public string Content { get; set; } = null!;
    
    public string Role = null!;
}

public class Agent
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Instructions { get; set; } = null!;
}

public class Thread
{
    public string Id { get; set; } = null!;
}