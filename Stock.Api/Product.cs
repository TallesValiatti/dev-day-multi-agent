namespace Stock.Api;

public class Stock
{
    public Guid Id { get; set; }
    public string ProductCode { get; set; } = null!;
    public int Quantity { get; set; }
}