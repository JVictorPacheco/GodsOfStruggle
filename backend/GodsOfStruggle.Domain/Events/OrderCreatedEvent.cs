using GodsOfStruggle.Domain.Common;

namespace GodsOfStruggle.Domain.Events;

public sealed record OrderCreatedEvent(Guid OrderId, Guid UserId) : DomainEvent;
