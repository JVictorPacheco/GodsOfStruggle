using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.ValueObjects;

namespace GodsOfStruggle.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string VariantDescription { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = Money.Zero;
    public Money TotalPrice { get; private set; } = Money.Zero;

    private OrderItem() { }

    public static OrderItem Create(Guid orderId, Guid productVariantId, string productName, string variantDescription, int quantity, Money unitPrice)
    {
        return new OrderItem
        {
            OrderId = orderId,
            ProductVariantId = productVariantId,
            ProductName = productName,
            VariantDescription = variantDescription,
            Quantity = quantity,
            UnitPrice = unitPrice,
            TotalPrice = unitPrice * quantity
        };
    }
}
