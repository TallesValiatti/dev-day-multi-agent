namespace OverviewAzureAiAgentService.SalesApi;

public class Product
{
    public Guid Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}