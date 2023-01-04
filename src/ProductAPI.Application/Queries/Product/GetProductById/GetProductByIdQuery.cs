using MediatR;
using ProductAPI.Application.Queries.Product.GetPaginatedProduct;

namespace ProductAPI.Application.Queries.Product.GetProductById;

public class GetProductByIdQuery : IRequest<ProductDTO>
{
    public int ProductId { get; set; }
}