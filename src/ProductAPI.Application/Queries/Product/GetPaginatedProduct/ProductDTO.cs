namespace ProductAPI.Application.Queries.Product.GetPaginatedProduct;

public class ProductDTO
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int InInventory { get; set; }
    public int MaxPurchase { get; set; }
    public int MinPurchase { get; set; }
}