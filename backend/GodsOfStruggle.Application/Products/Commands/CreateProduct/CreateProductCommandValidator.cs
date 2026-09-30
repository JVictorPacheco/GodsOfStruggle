using FluentValidation;

namespace GodsOfStruggle.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.BasePrice).GreaterThan(0);
        RuleFor(x => x.CategoryId).NotEmpty();
    }
}
