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
        return _mapper.Map<IEnumerable<Domain.Models.Purchase>, IEnumerable<PurchaseDTO>>(result);
    }
}
