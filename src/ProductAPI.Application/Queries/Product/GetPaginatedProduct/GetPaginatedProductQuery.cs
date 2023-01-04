using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Common.Models;

namespace ProductAPI.Application.Queries.Product.GetPaginatedProduct;

public class GetPaginatedProductQuery : IRequest<PaginationList<ProductDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}