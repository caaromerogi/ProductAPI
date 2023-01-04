using AutoMapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Common.Models;


namespace ProductAPI.Application.Queries.Product.GetPaginatedProduct;

public class GetPaginatedProductQueryHandler : IRequestHandler<GetPaginatedProductQuery, PaginationList<ProductDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPaginatedProductQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<PaginationList<ProductDTO>> Handle(GetPaginatedProductQuery request, CancellationToken cancellationToken)
    {
        var response = await _unitOfWork.ProductRepository.GetAll();
        var filteredResponse = from element in response
                            where element.IsEnabled == true
                            select element;
        var responseDTO = _mapper.Map<IEnumerable<Domain.Models.Product>, IEnumerable<ProductDTO>>(filteredResponse);
        
        return PaginationList<ProductDTO>.Create(responseDTO.AsQueryable(), request.PageNumber, request.PageSize);
        
    }
}
