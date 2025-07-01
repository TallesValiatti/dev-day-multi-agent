namespace OverviewAzureAiAgentService.SalesApi;

public class Products
{ 
    public static IEnumerable<Product> Items => new List<Product>()
    {
        new() { Id = Guid.NewGuid(), Code = "WH001", Name = "Wireless Headphones" },
        new() { Id = Guid.NewGuid(), Code = "CM002", Name = "Coffee Mug" },
        new() { Id = Guid.NewGuid(), Code = "BS003", Name = "Bluetooth Speaker" },
        new() { Id = Guid.NewGuid(), Code = "NS004", Name = "Notebook Set" },
        new() { Id = Guid.NewGuid(), Code = "WB005", Name = "Water Bottle" },
        new() { Id = Guid.NewGuid(), Code = "DL006", Name = "Desk Lamp" },
        new() { Id = Guid.NewGuid(), Code = "PC007", Name = "Phone Case" }
    };
}