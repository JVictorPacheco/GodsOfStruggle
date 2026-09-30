using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.ValueObjects;

namespace GodsOfStruggle.Domain.Entities;

public class CartItem : BaseEntity
{
    public Guid CartId { get; private set; }
    public Guid ProductVariantId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public string VariantDescription { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public Money UnitPrice { get; private set; } = Money.Zero;

    public Money TotalPrice => UnitPrice * Quantity;

    private CartItem() { }

    public static CartItem Create(Guid cartId, Guid productVariantId, string productName, string variantDescription, int quantity, Money unitPrice)
    {
        return new CartItem
        {
            CartId = cartId,
            ProductVariantId = productVariantId,
            ProductName = productName,
            VariantDescription = variantDescription,
            Quantity = quantity,
            UnitPrice = unitPrice
        };
    }

    public void UpdateQuantity(int quantity) { Quantity = quantity; UpdateTimestamp(); }
}
