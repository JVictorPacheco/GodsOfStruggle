using GodsOfStruggle.Domain.Enums;

namespace GodsOfStruggle.Domain.Exceptions;

public class InvalidOrderStatusTransitionException(OrderStatus current, OrderStatus next)
    : DomainException($"Transição de status inválida: {current} → {next}.");
