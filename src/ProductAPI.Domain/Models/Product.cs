namespace ProductAPI.Domain.Models;

public class Product 
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int InInventory { get; set; }
    public bool IsEnabled { get; set; }
    public int MinPurchase { get; set; }
    public int MaxPurchase { get; set; }
    public ICollection<Purchase> Purchases{get;set;}
}