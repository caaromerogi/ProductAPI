namespace ProductAPI.Domain.Models;

public class Product 
{
    public Guid Id { get; set; }
    public string ProductName { get; set; }
    public int InInventory { get; set; }
    public bool Enabled { get; set; }
    public int MinPurchase { get; set; }
    public int MaxPurchase { get; set; }

    public Product(Guid id, string productName, int inInventory, bool enabled, int minPurchase, int maxPurchase)
    {
        Id = id;
        ProductName = productName;
        InInventory = inInventory;
        Enabled = enabled;
        MinPurchase = minPurchase;
        MaxPurchase = maxPurchase;
    }

    public Product()
    {
    }
}