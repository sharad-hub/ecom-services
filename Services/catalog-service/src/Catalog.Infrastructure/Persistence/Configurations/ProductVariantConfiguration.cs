using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public sealed class ProductVariantConfiguration
    : IEntityTypeConfiguration<ProductVariant>
{
    public void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        builder.ToTable("product_variants");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(x => x.Amount)
                .HasColumnName("price_amount")
                .HasPrecision(18, 2);

            price.Property(x => x.Currency)
                .HasColumnName("price_currency")
                .HasMaxLength(3);
        });
    }
}