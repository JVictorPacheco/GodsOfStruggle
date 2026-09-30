namespace GodsOfStruggle.Application.Common.DTOs;

public record ProductImageDto(Guid Id, string Url, string AltText, int DisplayOrder, bool IsPrimary);

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal BasePrice,
    string Currency,
    Guid CategoryId,
    string? CategoryName,
    IReadOnlyList<ProductImageDto> Images,
    IReadOnlyList<ProductVariantDto> Variants,
    bool IsActive,
    DateTime CreatedAt);

public record ProductListItemDto(
    Guid Id,
    string Name,
    decimal BasePrice,
    string Currency,
    Guid CategoryId,
    string? CategoryName,
    string? PrimaryImageUrl,
    int VariantCount,
    bool IsActive);
