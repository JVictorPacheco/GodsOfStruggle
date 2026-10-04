using GodsOfStruggle.Application.Common.DTOs;

namespace GodsOfStruggle.Application.Cart;

public static class CartMapper
{
    public static CartDto ToDto(Domain.Entities.Cart cart) => new(
        cart.Id,
        cart.Items.Select(ToItemDto).ToList(),
        cart.Total.Amount,
        cart.Total.Currency,
        cart.Items.Sum(i => i.Quantity));

    private static CartItemDto ToItemDto(Domain.Entities.CartItem item) => new(
        item.Id,
        item.ProductVariantId,
        item.ProductName,
        item.VariantDescription,
        item.Quantity,
        item.UnitPrice.Amount,
        item.UnitPrice.Currency,
        item.TotalPrice.Amount);
}
