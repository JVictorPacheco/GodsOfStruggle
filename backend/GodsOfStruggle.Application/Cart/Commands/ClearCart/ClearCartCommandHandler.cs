using GodsOfStruggle.Application.Interfaces;
using MediatR;

namespace GodsOfStruggle.Application.Cart.Commands.ClearCart;

public class ClearCartCommandHandler(ICartRepository cartRepository, ICurrentUserService currentUser)
    : IRequestHandler<ClearCartCommand>
{
    public async Task Handle(ClearCartCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Cart? cart = await cartRepository.GetByUserIdAsync(currentUser.UserId, cancellationToken);
        if (cart is null) return;

        cart.Clear();
        await cartRepository.UpdateAsync(cart, cancellationToken);
    }
}
