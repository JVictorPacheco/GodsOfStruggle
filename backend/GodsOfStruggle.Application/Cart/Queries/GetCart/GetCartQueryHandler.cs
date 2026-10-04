using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Cart.Queries.GetCart;

public class GetCartQueryHandler(ICartRepository cartRepository, ICurrentUserService currentUser)
    : IRequestHandler<GetCartQuery, CartDto>
{
    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        Domain.Entities.Cart cart = await cartRepository.GetOrCreateByUserIdAsync(currentUser.UserId, cancellationToken);
        return CartMapper.ToDto(cart);
    }
}
