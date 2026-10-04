using GodsOfStruggle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GodsOfStruggle.Infrastructure.Persistence.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("cart_items");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.ProductVariantId).IsRequired();
        builder.Property(i => i.ProductName).IsRequired().HasMaxLength(200);
        builder.Property(i => i.VariantDescription).HasMaxLength(200);
        builder.Property(i => i.Quantity).IsRequired();
        builder.OwnsOne(i => i.UnitPrice, price =>
        {
            price.Property(p => p.Amount).HasColumnName("price").HasColumnType("numeric(18,2)").IsRequired();
            price.Property(p => p.Currency).HasColumnName("price_currency").HasMaxLength(3).IsRequired();
        });
        builder.Ignore(i => i.TotalPrice);
    }
}
