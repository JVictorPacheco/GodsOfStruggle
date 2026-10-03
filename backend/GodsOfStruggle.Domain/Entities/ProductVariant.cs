using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.Exceptions;
using GodsOfStruggle.Domain.ValueObjects;

namespace GodsOfStruggle.Domain.Entities;

public class ProductVariant : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string SKU { get; private set; } = string.Empty;
    public Dictionary<string, string> Attributes { get; private set; } = [];
    public Money Price { get; private set; } = Money.Zero;
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; } = true;

    private ProductVariant() { }

    public static ProductVariant Create(Guid productId, string sku, Dictionary<string, string> attributes, Money price, int stockQuantity)
    {
        return new ProductVariant
        {
            ProductId = productId,
            SKU = sku,
            Attributes = attributes,
            Price = price,
            StockQuantity = stockQuantity
        };
    }

    public void UpdateStock(int quantity)
    {
        StockQuantity = quantity;
        UpdateTimestamp();
    }

    public void DeductStock(int quantity)
    {
        if (StockQuantity < quantity)
            throw new InsufficientStockException(SKU, quantity, StockQuantity);

        StockQuantity -= quantity;
        UpdateTimestamp();
    }

    public void RestoreStock(int quantity)
    {
        StockQuantity += quantity;
        UpdateTimestamp();
    }

    public void UpdatePrice(Money price)
    {
        Price = price;
        UpdateTimestamp();
    }

    public void Deactivate() { IsActive = false; UpdateTimestamp(); }
    public void Activate() { IsActive = true; UpdateTimestamp(); }
}
