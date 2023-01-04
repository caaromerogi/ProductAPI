using MediatR;
using ProductAPI.Application.Common.Models;

namespace ProductAPI.Application.Commands.Product.UpdateProduct;

public class UpdateProductCommand : IRequest<ResponseModel<bool>>
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public int InInventory { get; set; }
    public int MinPurchase { get; set; }
    public int MaxPurchase { get; set; }
}