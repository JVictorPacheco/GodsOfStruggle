using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.ValueObjects;

namespace GodsOfStruggle.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid UserId { get; private set; }

    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();
    private readonly List<CartItem> _items = [];

    public Money Total => _items.Aggregate(Money.Zero, (acc, item) => acc + item.TotalPrice);

    private Cart() { }

    public static Cart Create(Guid userId)
    {
        return new Cart { UserId = userId };
    }

    public void AddItem(Guid productVariantId, string productName, string variantDescription, int quantity, Money unitPrice)
    {
        CartItem? existing = _items.FirstOrDefault(i => i.ProductVariantId == productVariantId);
        if (existing is not null)
        {
            existing.UpdateQuantity(existing.Quantity + quantity);
        }
        else
        {
            _items.Add(CartItem.Create(Id, productVariantId, productName, variantDescription, quantity, unitPrice));
        }
        UpdateTimestamp();
    }

    public void UpdateItemQuantity(Guid itemId, int quantity)
    {
        CartItem? item = _items.FirstOrDefault(i => i.Id == itemId);
        item?.UpdateQuantity(quantity);
        UpdateTimestamp();
    }

    public void RemoveItem(Guid itemId)
    {
        CartItem? item = _items.FirstOrDefault(i => i.Id == itemId);
        if (item is not null) { _items.Remove(item); UpdateTimestamp(); }
    }

    public void Clear() { _items.Clear(); UpdateTimestamp(); }
}
