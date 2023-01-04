using MediatR;
using ProductAPI.Application.Common.Models;

namespace ProductAPI.Application.Commands.Product.CreateProduct;

public class CreateProductCommand : IRequest<ResponseModel>
{
    public string ProductName { get; set; }
    public int InInventory { get; set; }
    public int MinPurchase { get; set; }
    public int MaxPurchase { get; set; }
}