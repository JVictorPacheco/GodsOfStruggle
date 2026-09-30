using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.Enums;

namespace GodsOfStruggle.Domain.Events;

public sealed record OrderStatusChangedEvent(Guid OrderId, OrderStatus Previous, OrderStatus Current) : DomainEvent;
