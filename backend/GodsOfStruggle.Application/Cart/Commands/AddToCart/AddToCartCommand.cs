using MediatR;

namespace GodsOfStruggle.Application.Cart.Commands.AddToCart;

public record AddToCartCommand(
    Guid ProductVariantId,
    string ProductName,
    string VariantDescription,
    int Quantity,
    decimal Price) : IRequest<Guid>;
