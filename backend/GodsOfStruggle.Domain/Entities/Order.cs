using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.Enums;
using GodsOfStruggle.Domain.Events;
using GodsOfStruggle.Domain.Exceptions;
using GodsOfStruggle.Domain.ValueObjects;

namespace GodsOfStruggle.Domain.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; private set; }
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public ShippingAddress ShippingAddress { get; private set; } = null!;
    public Money Subtotal { get; private set; } = Money.Zero;
    public Money ShippingFee { get; private set; } = Money.Zero;
    public Money Total { get; private set; } = Money.Zero;
    public PaymentMethod PaymentMethod { get; private set; }
    public PaymentStatus PaymentStatus { get; private set; } = PaymentStatus.Pending;
    public string? Notes { get; private set; }

    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    private readonly List<OrderItem> _items = [];

    private static readonly Dictionary<OrderStatus, IReadOnlyCollection<OrderStatus>> _validTransitions = new()
    {
        [OrderStatus.Pending]    = [OrderStatus.Confirmed, OrderStatus.Cancelled],
        [OrderStatus.Confirmed]  = [OrderStatus.Processing, OrderStatus.Cancelled],
        [OrderStatus.Processing] = [OrderStatus.Shipped, OrderStatus.Cancelled],
        [OrderStatus.Shipped]    = [OrderStatus.Delivered],
        [OrderStatus.Delivered]  = [],
        [OrderStatus.Cancelled]  = []
    };

    private Order() { }

    public static Order Create(Guid userId, ShippingAddress shippingAddress, PaymentMethod paymentMethod, string? notes = null)
    {
        Order order = new()
        {
            UserId = userId,
            ShippingAddress = shippingAddress,
            PaymentMethod = paymentMethod,
            Notes = notes
        };

        order.AddDomainEvent(new OrderCreatedEvent(order.Id, userId));
        return order;
    }

    public void AddItem(Guid productVariantId, string productName, string variantDescription, int quantity, Money unitPrice)
    {
        _items.Add(OrderItem.Create(Id, productVariantId, productName, variantDescription, quantity, unitPrice));
        RecalculateTotals();
    }

    public void SetShippingFee(Money shippingFee)
    {
        ShippingFee = shippingFee;
        RecalculateTotals();
    }

    public void UpdateStatus(OrderStatus newStatus)
    {
        if (!_validTransitions[Status].Contains(newStatus))
            throw new InvalidOrderStatusTransitionException(Status, newStatus);

        OrderStatus previous = Status;
        Status = newStatus;
        UpdateTimestamp();
        AddDomainEvent(new OrderStatusChangedEvent(Id, previous, newStatus));
    }

    public void MarkAsPaid()
    {
        PaymentStatus = PaymentStatus.Paid;
        UpdateTimestamp();
    }

    private void RecalculateTotals()
    {
        Subtotal = _items.Aggregate(Money.Zero, (acc, item) => acc + item.TotalPrice);
        Total = Subtotal + ShippingFee;
        UpdateTimestamp();
    }
}
