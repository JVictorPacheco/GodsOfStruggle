using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GodsOfStruggle.Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.ToTable("addresses");
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Label).IsRequired().HasMaxLength(50);
        builder.Property(a => a.Street).IsRequired().HasMaxLength(200);
        builder.Property(a => a.Number).IsRequired().HasMaxLength(20);
        builder.Property(a => a.Complement).HasMaxLength(100);
        builder.Property(a => a.Neighborhood).IsRequired().HasMaxLength(100);
        builder.Property(a => a.City).IsRequired().HasMaxLength(100);
        builder.Property(a => a.State).IsRequired().HasMaxLength(2);
        builder.Property(a => a.ZipCode).IsRequired().HasMaxLength(9);
    }
}
