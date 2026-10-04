using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Cart.Queries.GetCart;

public record GetCartQuery : IRequest<CartDto>;
