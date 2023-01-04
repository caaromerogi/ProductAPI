using AutoMapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Common.Models;
using ProductAPI.Domain.Models;

namespace ProductAPI.Application.Commands.Purchase.CreatePurchase;

public class CreatePurchaseCommandHandler : IRequestHandler<CreatePurchaseCommand, ResponseModel>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreatePurchaseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel> Handle(CreatePurchaseCommand request, 
    CancellationToken cancellationToken)
    {   
        var productPurchases = new List<ProductPurchase>();
        var products = new List<Domain.Models.Product>();
        foreach (var item in request.Products)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(item.ProductId);
            product.InInventory = product.InInventory - item.Quantity;
            products.Add(product);
            var pp = new ProductPurchase();
            pp.ProductId = item.ProductId;
            pp.Quantity = item.Quantity;
            productPurchases.Add(pp);
        }  

        var purchase = new Domain.Models.Purchase();
        purchase.Date = DateTime.Now;
        purchase.Products = products;
        purchase.IdType = request.IdType;
        purchase.IdNumber = request.IdNumber;
        purchase.ClientName = request.ClientName;
        
        await _unitOfWork.PurchaseRepository.AddAsync(purchase);
        
        await _unitOfWork.SaveChangesAsync();



        foreach(var item in request.Products)
        {
            var editPurchase = await _unitOfWork.ProductPurchaseRepository.GetByIdAsync(item.ProductId, purchase.PurchaseId);
            var prdd = request.Products.First(p => p.ProductId == editPurchase.ProductId);
            editPurchase.Quantity = prdd.Quantity;
            
        }
        await _unitOfWork.SaveChangesAsync();
        

        return new ResponseModel("Purchase successfully added");
    }
}
