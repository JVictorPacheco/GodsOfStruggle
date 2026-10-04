using CartEntity = GodsOfStruggle.Domain.Entities.Cart;

namespace GodsOfStruggle.Application.Interfaces;

public interface ICartRepository
{
    Task<CartEntity> GetOrCreateByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<CartEntity?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task UpdateAsync(CartEntity cart, CancellationToken ct = default);
}
