using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GodsOfStruggle.Infrastructure.Persistence.Repositories;

public class CartRepository(AppDbContext context) : ICartRepository
{
    public async Task<Cart> GetOrCreateByUserIdAsync(Guid userId, CancellationToken ct = default)
    {
        Cart? cart = await GetByUserIdAsync(userId, ct);
        if (cart is not null) return cart;

        Cart newCart = Cart.Create(userId);
        context.Carts.Add(newCart);
        await context.SaveChangesAsync(ct);
        return newCart;
    }

    public async Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken ct = default)
        => await context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId, ct);

    public async Task UpdateAsync(Cart cart, CancellationToken ct = default)
    {
        context.Carts.Update(cart);
        await context.SaveChangesAsync(ct);
    }
}
