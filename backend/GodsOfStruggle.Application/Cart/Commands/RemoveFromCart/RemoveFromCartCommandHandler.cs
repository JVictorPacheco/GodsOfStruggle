using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Exceptions;
using MediatR;

namespace GodsOfStruggle.Application.Cart.Commands.RemoveFromCart;

public class RemoveFromCartCommandHandler(ICartRepository cartRepository, ICurrentUserService currentUser)
    : IRequestHandler<RemoveFromCartCommand>
{
    public async Task Handle(RemoveFromCartCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Cart? cart = await cartRepository.GetByUserIdAsync(currentUser.UserId, cancellationToken);
        if (cart is null) throw new NotFoundException("Carrinho não encontrado.");

        cart.RemoveItem(request.CartItemId);
        await cartRepository.UpdateAsync(cart, cancellationToken);
    }
}
