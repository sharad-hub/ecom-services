using Catalog.Domain.Aggregates;
using CatalogService.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.Configurations;

public sealed class ProductConfiguration
    : IEntityTypeConfiguration<Product>
{
    public void Configure(
        EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(5000);

        builder.Property(x => x.CategoryId)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasConversion<string>();

        //builder.Property(x => x.Sku)
        //    .HasConversion(
        //        sku => sku.Value,
        //        value => Sku.Create(value).Value)
        //    .HasMaxLength(50);
        builder.Property(x => x.Sku)
            .HasMaxLength(50)
            .IsRequired();

        //builder.HasIndex(x => x.Sku)
        //    .IsUnique();

        builder.HasIndex(x => x.Sku)
            .IsUnique();

        builder.Property<bool>("IsDeleted")
       .HasDefaultValue(false);

        builder.OwnsOne(x => x.Price, price =>
        {
            price.Property(p => p.Amount)
                .HasColumnName("price_amount")
                .HasPrecision(18, 2);

            price.Property(p => p.Currency)
                .HasColumnName("price_currency")
                .HasMaxLength(3);
        });

        builder.HasQueryFilter(
            x => !x.IsDeleted);
        builder.Ignore(x => x.DomainEvents);

        builder
    .HasOne(x => x.Category)
    .WithMany(x => x.Products)
    .HasForeignKey(x => x.CategoryId);

    //    builder
    //.HasOne<Category>()
    //.WithMany()
    //.HasForeignKey(x => x.CategoryId);
    }
}