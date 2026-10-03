using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.ValueObjects;

namespace GodsOfStruggle.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public Money BasePrice { get; private set; } = Money.Zero;
    public Guid CategoryId { get; private set; }
    public bool IsActive { get; private set; } = true;
    public float[]? Embedding { get; private set; }

    public Category? Category { get; private set; }
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();
    public IReadOnlyCollection<ProductImage> Images => _images.AsReadOnly();

    private readonly List<ProductVariant> _variants = [];
    private readonly List<ProductImage> _images = [];

    private Product() { }

    public static Product Create(string name, string description, Money basePrice, Guid categoryId)
    {
        return new Product
        {
            Name = name,
            Description = description,
            BasePrice = basePrice,
            CategoryId = categoryId
        };
    }

    public void Update(string name, string description, Money basePrice, Guid categoryId)
    {
        Name = name;
        Description = description;
        BasePrice = basePrice;
        CategoryId = categoryId;
        UpdateTimestamp();
    }

    public ProductVariant AddVariant(string sku, Dictionary<string, string> attributes, Money price, int stockQuantity)
    {
        ProductVariant variant = ProductVariant.Create(Id, sku, attributes, price, stockQuantity);
        _variants.Add(variant);
        UpdateTimestamp();
        return variant;
    }

    public void RemoveVariant(Guid variantId)
    {
        ProductVariant? variant = _variants.FirstOrDefault(v => v.Id == variantId);
        if (variant is not null)
        {
            _variants.Remove(variant);
            UpdateTimestamp();
        }
    }

    public void AddImage(string url, string altText, int displayOrder = 0, bool isPrimary = false)
    {
        ProductImage image = ProductImage.Create(Id, url, altText, displayOrder, isPrimary);
        _images.Add(image);
        UpdateTimestamp();
    }

    public void SetEmbedding(float[] embedding)
    {
        Embedding = embedding;
        UpdateTimestamp();
    }

    public void Deactivate() { IsActive = false; UpdateTimestamp(); }
    public void Activate() { IsActive = true; UpdateTimestamp(); }
}
