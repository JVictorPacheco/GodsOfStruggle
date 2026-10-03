using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GodsOfStruggle.Infrastructure.Persistence.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public Task<List<Category>> GetAllActiveAsync(CancellationToken cancellationToken = default) =>
        context.Categories.Where(c => c.IsActive).OrderBy(c => c.Name).ToListAsync(cancellationToken);

    public Task<Category?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Categories.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public Task<Category?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default) =>
        context.Categories.FirstOrDefaultAsync(c => c.Slug == slug, cancellationToken);

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
        await context.Categories.AddAsync(category, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Category category, CancellationToken cancellationToken = default)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync(cancellationToken);
    }
}
