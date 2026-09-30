using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Domain.Entities;

namespace GodsOfStruggle.Application.Products.Commands.CreateProduct;

public static class ProductMapper
{
    public static ProductDto ToDto(Product p, string? categoryName = null) => new(
        p.Id, p.Name, p.Description,
        p.BasePrice.Amount, p.BasePrice.Currency,
        p.CategoryId, categoryName,
        p.Images.Select(i => new ProductImageDto(i.Id, i.Url, i.AltText, i.DisplayOrder, i.IsPrimary)).ToList(),
        p.Variants.Select(v => new ProductVariantDto(v.Id, v.SKU, v.Attributes, v.Price.Amount, v.Price.Currency, v.StockQuantity, v.IsActive)).ToList(),
        p.IsActive, p.CreatedAt);

    public static ProductListItemDto ToListItemDto(Product p, string? categoryName = null) => new(
        p.Id, p.Name,
        p.BasePrice.Amount, p.BasePrice.Currency,
        p.CategoryId, categoryName,
        p.Images.FirstOrDefault(i => i.IsPrimary)?.Url ?? p.Images.FirstOrDefault()?.Url,
        p.Variants.Count, p.IsActive);
}
