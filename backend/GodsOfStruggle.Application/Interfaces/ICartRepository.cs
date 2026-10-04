using GodsOfStruggle.Domain.Entities;

namespace GodsOfStruggle.Application.Interfaces;

public interface ICartRepository
{
    Task<Cart> GetOrCreateByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task<Cart?> GetByUserIdAsync(Guid userId, CancellationToken ct = default);
    Task UpdateAsync(Cart cart, CancellationToken ct = default);
}
