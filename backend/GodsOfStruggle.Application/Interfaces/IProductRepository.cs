using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Domain.Entities;

namespace GodsOfStruggle.Application.Interfaces;

public interface IProductRepository
{
    Task<PagedResult<Product>> GetPagedAsync(string? search, Guid? categoryId, decimal? minPrice, decimal? maxPrice, int page, int pageSize, CancellationToken cancellationToken = default);
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
}
