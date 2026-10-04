namespace GodsOfStruggle.Application.Common.DTOs;

public record CartItemDto(
    Guid Id,
    Guid ProductVariantId,
    string ProductName,
    string VariantDescription,
    int Quantity,
    decimal UnitPrice,
    string Currency,
    decimal TotalPrice);

public record CartDto(
    Guid Id,
    List<CartItemDto> Items,
    decimal Total,
    string Currency,
    int ItemCount);
