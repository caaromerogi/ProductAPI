using MediatR;
using ProductAPI.Application.Common.Models;

namespace ProductAPI.Application.Commands.Product.CreateProduct;

public class CreateProductCommand : IRequest<ResponseModel<bool>>
{
    public string ProductName { get; set; }
    public int InInventory { get; set; }
    public bool? IsEnabled { get; set; } = true;
    public int MinPurchase { get; set; }
    public int MaxPurchase { get; set; }
    public CreateProductCommand(string productName, int inInventory, int minPurchase, int maxPurchase)
    {
        ProductName = productName;
        InInventory = inInventory;
        MinPurchase = minPurchase;
        MaxPurchase = maxPurchase;
    }
}