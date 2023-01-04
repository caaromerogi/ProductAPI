using AutoMapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Common.Models;

namespace ProductAPI.Application.Commands.Purchase.CreatePurchase;

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, ResponseModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePurchaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<bool>> Handle(CreatePurchaseCommand request, 
    CancellationToken cancellationToken)
    {   
        var purchase = _mapper.Map<CreatePurchaseCommand, Domain.Models.Purchase>(request);
        await _unitOfWork.PurchaseRepository.AddAsync(purchase);
        foreach (var item in request.Products)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
            product.InInventory = product.InInventory - item.Quantity;
        }
        await _unitOfWork.SaveChangesAsync();
        return new ResponseModel<bool>(true, "Purchase successfully added");
    }
}
