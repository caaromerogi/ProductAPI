using MediatR;
using ProductAPI.Application.Common.Models;

namespace ProductAPI.Application.Commands.Purchase.CreatePurchase;

public class CreatePurchaseCommand : IRequest<ResponseModel<bool>>
{   
    public string IdType { get; set; }
    public string IdNumber { get; set; }
    public string ClientName{ get; set; }
    public IEnumerable<ProductPurchaseModel> Products { get; set;}
}