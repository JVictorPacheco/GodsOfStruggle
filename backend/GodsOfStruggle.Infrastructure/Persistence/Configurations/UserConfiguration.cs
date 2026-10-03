using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GodsOfStruggle.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedNever();
        builder.Property(u => u.Email).IsRequired().HasMaxLength(256);
        builder.Property(u => u.Name).IsRequired().HasMaxLength(200);
        builder.Property(u => u.Phone).HasMaxLength(20);
        builder.Property(u => u.Role)
            .HasConversion(r => r.ToString(), s => Enum.Parse<UserRole>(s))
            .HasMaxLength(20);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Navigation(u => u.Addresses).HasField("_addresses");
        builder.HasMany(u => u.Addresses)
            .WithOne()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
