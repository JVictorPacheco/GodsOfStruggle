using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.ValueObjects;
using MediatR;

namespace GodsOfStruggle.Application.Cart.Commands.AddToCart;

public class AddToCartCommandHandler(ICartRepository cartRepository, ICurrentUserService currentUser)
    : IRequestHandler<AddToCartCommand, Guid>
{
    public async Task<Guid> Handle(AddToCartCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Cart cart = await cartRepository.GetOrCreateByUserIdAsync(currentUser.UserId, cancellationToken);
        cart.AddItem(request.ProductVariantId, request.ProductName, request.VariantDescription, request.Quantity, new Money(request.Price));
        await cartRepository.UpdateAsync(cart, cancellationToken);
        return cart.Id;
    }
}
