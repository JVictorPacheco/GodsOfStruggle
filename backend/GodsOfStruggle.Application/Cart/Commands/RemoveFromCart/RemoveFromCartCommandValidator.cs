using FluentValidation;

namespace GodsOfStruggle.Application.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommandValidator : AbstractValidator<RemoveFromCartCommand>
{
    public RemoveFromCartCommandValidator()
    {
        RuleFor(x => x.CartItemId).NotEmpty();
    }
}
