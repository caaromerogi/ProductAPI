using ProductAPI.Application.Queries.Product.GetPaginatedProduct;

namespace ProductAPI.Application.Queries.Purchase;

public class PurchaseDTO
{
    public int PurchaseId { get; set; }
    public DateTime Date { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public string ClientName{ get; set; }
    public IEnumerable<ProductPurchaseDTO> Products { get; set; }
}