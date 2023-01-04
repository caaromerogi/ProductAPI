using AutoMapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;

namespace ProductAPI.Application.Queries.Purchase.GetPurchase;

public class GetPurchaseQueryHandler : IRequestHandler<GetPurchaseQuery, IEnumerable<PurchaseDTO>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetPurchaseQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PurchaseDTO>> Handle(GetPurchaseQuery request, CancellationToken cancellationToken)
    {   
        var result = await _unitOfWork.PurchaseRepository.GetAll(includeProperties: "Products");
        var dto = _mapper.Map<IEnumerable<Domain.Models.Purchase>, IEnumerable<PurchaseDTO>>(result);
        
        foreach (var purc in dto)
        {
            var productPurchaseDTO = new List<ProductPurchaseDTO>();
            foreach (var prod in purc.Products)
            {
                var purchaseProduct = await _unitOfWork.ProductPurchaseRepository.GetByIdAsync(prod.ProductId, purc.PurchaseId);
                var prodToAdd = new ProductPurchaseDTO();
                prodToAdd.ProductName = prod.ProductName;
                prodToAdd.ProductId = prod.ProductId;
                prodToAdd.Quantity = purchaseProduct.Quantity;
                productPurchaseDTO.Add(prodToAdd);
            }

            purc.Products = productPurchaseDTO;
        }
        
    
        return dto;
    }
}
