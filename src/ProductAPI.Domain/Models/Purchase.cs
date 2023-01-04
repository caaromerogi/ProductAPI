namespace ProductAPI.Domain.Models;

public class Purchase
{
    public int PurchaseId { get; set; }
    public DateTime Date { get; set; }
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public string ClientName{ get; set; }
    public ICollection<Product> Products {get;set;} 
}

//foreach -> busca la entidad, le cambia la cantidad en inventario y guarda