using MediatR;

namespace GodsOfStruggle.Application.Cart.Commands.RemoveFromCart;

public record RemoveFromCartCommand(Guid CartItemId) : IRequest;
