using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.Enums;
using GodsOfStruggle.Domain.ValueObjects;


namespace GodsOfStruggle.Application.Common.DTOs;

public record OrderItemDto(
Guid UserId,
OrderStatus OrderStatus,
);
