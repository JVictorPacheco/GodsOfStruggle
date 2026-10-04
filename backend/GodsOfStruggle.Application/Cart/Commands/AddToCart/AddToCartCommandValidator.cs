using FluentValidation;

namespace GodsOfStruggle.Application.Cart.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(x => x.ProductVariantId).NotEmpty();
        RuleFor(x => x.ProductName).NotEmpty().MaximumLength(200);
        RuleFor(x => x.VariantDescription).MaximumLength(200);
        RuleFor(x => x.Quantity).InclusiveBetween(1, 99);
        RuleFor(x => x.Price).GreaterThan(0).LessThan(1_000_000);
    }
}
