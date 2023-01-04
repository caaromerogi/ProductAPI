using FluentValidation;

namespace ProductAPI.Application.Commands.Product.UpdateProduct;

public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(p => p.ProductName).NotEmpty();
        RuleFor(p => p.InInventory).GreaterThanOrEqualTo(0);
        RuleFor(p => p.MaxPurchase)
        .GreaterThanOrEqualTo(0)
        .LessThanOrEqualTo(p => p.InInventory);
        RuleFor(p => p.MinPurchase)
        .GreaterThanOrEqualTo(0)
        .LessThanOrEqualTo(p => p.InInventory);
        RuleFor(p => p.MinPurchase).LessThanOrEqualTo(p => p.MaxPurchase);
    }
}