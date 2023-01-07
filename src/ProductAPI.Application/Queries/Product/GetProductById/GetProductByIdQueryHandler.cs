using AutoMapper;
using Dapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Queries.Product.GetPaginatedProduct;
using ProductAPI.Domain.Exceptions.Implementation;

namespace ProductAPI.Application.Queries.Product.GetProductById;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
{
    private readonly IDapperService<Domain.Models.Product> _dapperService;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IDapperService<Domain.Models.Product> dapperService,
    IMapper mapper)
    {
        _dapperService = dapperService;
        _mapper = mapper;
    }

    public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {   
        var dictionary = new Dictionary<string, object>
        {
            {"@id", request.ProductId}
        };
        var parameters = new DynamicParameters(dictionary);
        var result = await _dapperService.Get(@"SELECT * FROM Product WHERE ProductId = @id", parameters);
        var product = result.FirstOrDefault<Domain.Models.Product>();

        if(product is null){
            throw new ProductNotFoundException($"The product with Id: {request.ProductId} wasn't found");
        }
        

        return _mapper.Map<Domain.Models.Product, ProductDTO>(product);
    }
}
