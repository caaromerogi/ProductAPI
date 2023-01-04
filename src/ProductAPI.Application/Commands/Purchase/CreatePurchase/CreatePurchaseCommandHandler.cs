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
        var products = new List<Domain.Models.Product>();
        foreach (var item in request.Products)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
            product.InInventory = product.InInventory - item.Quantity;
            products.Add(product);
        }

        var purchase = new Domain.Models.Purchase();
        purchase.Date = DateTime.Now;
        purchase.Products = products;
        purchase.IdType = request.IdType;
        purchase.IdNumber = request.IdNumber;
        purchase.ClientName = request.ClientName;
        
        await _unitOfWork.PurchaseRepository.AddAsync(purchase);

        
        await _unitOfWork.SaveChangesAsync();
        return new ResponseModel<bool>(true, "Purchase successfully added");
    }
}
