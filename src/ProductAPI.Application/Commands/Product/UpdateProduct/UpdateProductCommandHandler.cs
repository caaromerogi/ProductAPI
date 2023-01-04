using AutoMapper;
using MediatR;
using ProductAPI.Application.Common.Interfaces;
using ProductAPI.Application.Common.Models;
using ProductAPI.Domain.Exceptions.Implementation;

namespace ProductAPI.Application.Commands.Product.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseModel<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ResponseModel<bool>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    { 
        var productEntity = await _unitOfWork.ProductRepository.GetByIdAsync(request.ProductId);
        
        if(productEntity is null){
            throw new ProductNotFoundException($"The product with Id: {request.ProductId} wasn't found");
        }

        productEntity.InInventory = request.InInventory;
        productEntity.ProductName = request.ProductName;
        productEntity.MaxPurchase = request.MaxPurchase;
        productEntity.MinPurchase = request.MinPurchase;

        await _unitOfWork.SaveChangesAsync();

        return new ResponseModel<bool>(true, "Product successfully updated");
    }
}
