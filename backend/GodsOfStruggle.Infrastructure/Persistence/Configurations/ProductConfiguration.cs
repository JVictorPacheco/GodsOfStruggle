using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GodsOfStruggle.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).HasMaxLength(2000);
        builder.Ignore(p => p.Embedding);

        builder.OwnsOne(p => p.BasePrice, price =>
        {
            price.Property(m => m.Amount).HasColumnName("base_price").HasColumnType("numeric(10,2)");
            price.Property(m => m.Currency).HasColumnName("price_currency").HasMaxLength(3).HasDefaultValue("BRL");
        });

        builder.HasOne(p => p.Category).WithMany().HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Restrict);

        builder.Navigation(p => p.Variants).HasField("_variants");
        builder.HasMany(p => p.Variants).WithOne().HasForeignKey(v => v.ProductId).OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(p => p.Images).HasField("_images");
        builder.HasMany(p => p.Images).WithOne().HasForeignKey(i => i.ProductId).OnDelete(DeleteBehavior.Cascade);
    }
}
