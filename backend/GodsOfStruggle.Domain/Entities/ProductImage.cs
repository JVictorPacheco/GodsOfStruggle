using GodsOfStruggle.Domain.Common;

namespace GodsOfStruggle.Domain.Entities;

public class ProductImage : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string Url { get; private set; } = string.Empty;
    public string AltText { get; private set; } = string.Empty;
    public int DisplayOrder { get; private set; }
    public bool IsPrimary { get; private set; }

    private ProductImage() { }

    public static ProductImage Create(Guid productId, string url, string altText, int displayOrder = 0, bool isPrimary = false)
    {
        return new ProductImage
        {
            ProductId = productId,
            Url = url,
            AltText = altText,
            DisplayOrder = displayOrder,
            IsPrimary = isPrimary
        };
    }
}
