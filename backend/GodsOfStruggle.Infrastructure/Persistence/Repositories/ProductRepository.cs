using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GodsOfStruggle.Infrastructure.Persistence.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<PagedResult<Product>> GetPagedAsync(string? search, Guid? categoryId, decimal? minPrice, decimal? maxPrice, int page, int pageSize, CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query = context.Products
            .Include(p => p.Images)
            .Include(p => p.Variants)
            .Where(p => p.IsActive);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId.Value);

        if (minPrice.HasValue)
            query = query.Where(p => p.BasePrice.Amount >= minPrice.Value);

        if (maxPrice.HasValue)
            query = query.Where(p => p.BasePrice.Amount <= maxPrice.Value);

        int total = await query.CountAsync(cancellationToken);
        List<Product> items = await query
            .OrderByDescending(p => p.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<Product>(items, total, page, pageSize);
    }

    public Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Products
            .Include(p => p.Images)
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await context.Products.AddAsync(product, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Update(product);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        context.Products.Remove(product);
        await context.SaveChangesAsync(cancellationToken);
    }
}
