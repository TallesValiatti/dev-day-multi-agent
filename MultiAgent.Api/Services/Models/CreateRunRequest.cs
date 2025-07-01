namespace MultiAgent.Api.Services.Models;

public record CreateRunRequest(string AgentId, string ThreadId, string Message);