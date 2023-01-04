using FluentValidation;
using ProductAPI.Application.Common.Interfaces;

namespace ProductAPI.Application.Commands.Purchase.CreatePurchase;

public class CreatePurchaseValidator : AbstractValidator<CreatePurchaseCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurchaseValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleForEach(p => p.Products)
        .MustAsync((p, t)  => ValidateMin(p, _unitOfWork));

        RuleForEach(p=> p.Products)
        .MustAsync((p, t)  => ValidateMax(p, _unitOfWork));
        
    }

    private async Task<bool> ValidateMin(ProductPurchaseModel productModel, IUnitOfWork unitOfWork)
    {
        var p = await unitOfWork.ProductRepository.GetByIdAsync(productModel.ProductId);
        return productModel.Quantity>p.MinPurchase;
    }

    private async Task<bool> ValidateMax(ProductPurchaseModel productModel, IUnitOfWork unitOfWork)
    {
        var p = await unitOfWork.ProductRepository.GetByIdAsync(productModel.ProductId);
        return productModel.Quantity<p.MaxPurchase;
    }
    
}