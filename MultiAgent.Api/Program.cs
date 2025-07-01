using Microsoft.AspNetCore.Mvc;
using MultiAgent.Api.Services;
using MultiAgent.Api.Services.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddScoped<AgentService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");
app.MapOpenApi();
app.UseHttpsRedirection();

app.MapPost("/agents", async ([FromServices] AgentService service, CreateAgentRequest request) => 
    Results.Ok((object?)await service.CreateAgentAsync(request))).WithName("CreateAgent");

app.MapPost("/threads", async ([FromServices] AgentService service) => 
    await service.CreateThreadAsync())
    .WithName("CreateThread");

app.MapGet("/threads/{threadId}/messages", async ([FromServices] AgentService service, string threadId) =>
    await service.ListMessagesAsync(threadId))
    .WithName("ListThreadMessages");

app.MapPost("/run", async ([FromServices] AgentService service, CreateRunRequest request) =>
    await service.CreateRunAsync(request))
    .WithName("CreateRun");
    
app.Run();