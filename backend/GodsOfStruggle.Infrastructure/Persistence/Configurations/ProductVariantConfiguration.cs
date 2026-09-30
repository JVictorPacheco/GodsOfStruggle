using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace GodsOfStruggle.Infrastructure.Persistence.Configurations;

public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("product_variants");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.SKU).IsRequired().HasMaxLength(50);
        builder.HasIndex(v => v.SKU).IsUnique();

        builder.Property(v => v.Attributes).HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, (JsonSerializerOptions?)null)!);

        builder.OwnsOne(v => v.Price, price =>
        {
            price.Property(m => m.Amount).HasColumnName("price").HasColumnType("numeric(10,2)");
            price.Property(m => m.Currency).HasColumnName("price_currency").HasMaxLength(3).HasDefaultValue("BRL");
        });
    }
}
