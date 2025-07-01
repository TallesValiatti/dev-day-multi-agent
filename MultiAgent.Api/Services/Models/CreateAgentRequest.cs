namespace MultiAgent.Api.Services.Models;

public record CreateAgentRequest(
    string Name, 
    string Instructions);