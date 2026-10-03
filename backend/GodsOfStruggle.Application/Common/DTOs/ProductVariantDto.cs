namespace GodsOfStruggle.Application.Common.DTOs;

public record ProductVariantDto(
    Guid Id,
    string SKU,
    Dictionary<string, string> Attributes,
    decimal Price,
    string Currency,
    int StockQuantity,
    bool IsActive);
