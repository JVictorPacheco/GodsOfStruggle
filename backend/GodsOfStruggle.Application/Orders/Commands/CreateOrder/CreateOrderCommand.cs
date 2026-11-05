using MediatR;
using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Domain.Enums;

namespace GodsOfStruggle.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(List<OrderItemDto> OrderItems, ShippingAddressDto ShippingAddress, PaymentMethod PaymentMethod) : IRequest<Guid>;
