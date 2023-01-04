using MediatR;
using ProductAPI.Application.Common.Models;

namespace ProductAPI.Application.Commands.Product.DeleteProduct;

public class DeleteProductCommand : IRequest<ResponseModel>
{
    public int ProductId { get; set; }
}