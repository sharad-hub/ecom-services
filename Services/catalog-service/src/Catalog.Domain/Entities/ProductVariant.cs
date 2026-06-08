using CatalogService.Domain.Common;
using CatalogService.Domain.ValueObjects;

namespace CatalogService.Domain.Entities;

public sealed class ProductVariant : Entity<Guid>
{
    public string Title { get; private set; } = default!;

    // public Sku Sku { get; private set; } = default!;

    public string Sku { get; private set; } = string.Empty;
    public Money Price { get; private set; } = default!;

    public bool IsActive { get; private set; }

    private ProductVariant()
    {
    }

    public ProductVariant(
        Guid id,
        string title,
        string sku,
        Money price)
    {
        Id = id;
        Title = title;
        Sku = sku;
        Price = price;
        IsActive = true;
    }
}