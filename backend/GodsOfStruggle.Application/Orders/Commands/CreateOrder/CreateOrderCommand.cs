using MediatR;

namespace GodsOfStruggle.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderStatus Status, ShippingAddressDto ShippingAddress) : IRequest<Guid>;
