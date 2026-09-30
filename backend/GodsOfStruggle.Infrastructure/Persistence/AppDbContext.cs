using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GodsOfStruggle.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Address> Addresses => Set<Address>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (Microsoft.EntityFrameworkCore.Metadata.IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes()
            .Where(e => typeof(BaseEntity).IsAssignableFrom(e.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType).Ignore(nameof(BaseEntity.DomainEvents));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
