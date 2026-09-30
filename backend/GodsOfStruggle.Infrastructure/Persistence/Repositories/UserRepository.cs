using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GodsOfStruggle.Infrastructure.Persistence.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Users.Include(u => u.Addresses).FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

    public Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default) =>
        context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.Users.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Users.AnyAsync(u => u.Id == id, cancellationToken);
}
