namespace ProductAPI.Domain.Models;

public class Purchase
{
    public int PurchaseId { get; set; }
    public DateTime Date { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public string ClientName{ get; set; }
    public ICollection<ProductPurchase> ProductPurchases {get;set;} 
}