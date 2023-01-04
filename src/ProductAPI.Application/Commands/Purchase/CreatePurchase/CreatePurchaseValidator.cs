using FluentValidation;
using ProductAPI.Application.Common.Interfaces;

namespace ProductAPI.Application.Commands.Purchase.CreatePurchase;

public class CreatePurchaseValidator : AbstractValidator<CreatePurchaseCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePurchaseValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        RuleFor(p => p.ClientName).NotEmpty();
        RuleFor(p => p.IdNumber).NotEmpty();
        RuleFor(p => p.IdType).NotEmpty();
        RuleForEach(p => p.Products)
        .MustAsync( async (p,pm, c, t)  =>{
            var prd = await _unitOfWork.ProductRepository.GetByIdAsync(pm.ProductId);
            c.MessageFormatter.AppendArgument("MinElements", prd.MinPurchase);
            return pm.Quantity>=prd.MinPurchase;
        }).WithMessage("The minimun purchase for this product is {MinElements} items");

        RuleForEach(p=> p.Products)
        .MustAsync(async (p,pm,c, t)  => {
            var prd = await _unitOfWork.ProductRepository.GetByIdAsync(pm.ProductId);
            c.MessageFormatter.AppendArgument("MaxElements", prd.MaxPurchase);  
            return pm.Quantity<=prd.MaxPurchase;
        }).WithMessage("The maximun purchase for this product is {MaxElements} items");

        RuleForEach(p => p.Products)
        .MustAsync(async (p, pm,c, t) => {
            var prd = await _unitOfWork.ProductRepository.GetByIdAsync(pm.ProductId);
            c.MessageFormatter.AppendArgument("ItemsAvailable", prd.InInventory);  
            return pm.Quantity>=prd.InInventory;
        }).WithMessage("There are only {ItemsAvailable} items available");
        
    }
    
}