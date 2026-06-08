using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using CatalogService.Domain.Enums;
using CatalogService.Domain.Errors;
using CatalogService.Domain.Events;
using CatalogService.Domain.ValueObjects;

namespace CatalogService.Domain.Aggregates;

/// <summary>
/// Domain class representing a product in the catalog.
/// It includes properties such as name, SKU, price, category, status, and description.
/// The class also contains domain methods for creating, updating, activating, and archiving products. It raises domain events when a product is created. 
/// The class is designed
/// </summary>
public sealed class Product : AggregateRoot<Guid>
{
    private readonly List<ProductVariant> _variants = [];

    public string Name { get; private set; } = string.Empty;

    public string Sku { get; private set; } = string.Empty;

    public Money Price { get; private set; } = default!;

    public Guid CategoryId { get; private set; }

    public ProductStatus Status { get; private set; }

    public string? Description { get; private set; }


    public bool IsDeleted { get; private set; }

    public DateTime? DeletedOnUtc { get; private set; }

    public IReadOnlyCollection<ProductVariant> Variants =>
        _variants.AsReadOnly();

    private Product()
    {
    }

    public Result Update(
    string name,
    Money price,
    string? description)
    {
        Name = name;
        Price = price;
        Description = description;

        return Result.Success();
    }
    public static Result<Product> Create(
        string name,
        string sku,
        Money price,
        Guid categoryId,
        string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result<Product>.Failure(
                ProductErrors.NameRequired);
        }

        if (string.IsNullOrWhiteSpace(sku))
        {
            return Result<Product>.Failure(
                ProductErrors.InvalidSku);
        }

        if (categoryId == Guid.Empty)
        {
            return Result<Product>.Failure(
                ProductErrors.CategoryRequired);
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Sku = sku.Trim().ToUpperInvariant(),
            Price = price,
            CategoryId = categoryId,
            Description = description,
            Status = ProductStatus.Draft
        };

        product.RaiseDomainEvent(
            new ProductCreatedEvent(
                product.Id,
                product.Sku));

        return Result<Product>.Success(product);
    }

    public Result UpdatePrice(Money newPrice)
    {
        Price = newPrice;
        return Result.Success();
    }

    public Result Activate()
    {
        Status = ProductStatus.Active;
        return Result.Success();
    }

    public Result Archive()
    {
        Status = ProductStatus.Archived;
        return Result.Success();
    }
    public Result Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;

        return Result.Success();
    }
}