namespace Stock.Api;

public class Stocks
{ 
    public static IEnumerable<Stock> Items => new List<Stock>()
    {
        new() { Id = Guid.NewGuid(), ProductCode = "WH001", Quantity = 0 },
        new() { Id = Guid.NewGuid(), ProductCode = "CM002", Quantity = 12 },
        new() { Id = Guid.NewGuid(), ProductCode = "BS003", Quantity = 76 },
        new() { Id = Guid.NewGuid(), ProductCode = "NS004", Quantity = 50 },
        new() { Id = Guid.NewGuid(), ProductCode = "WB005", Quantity = 10 },
        new() { Id = Guid.NewGuid(), ProductCode = "DL006", Quantity = 66 },
        new() { Id = Guid.NewGuid(), ProductCode = "PC007", Quantity = 24}
    };
}