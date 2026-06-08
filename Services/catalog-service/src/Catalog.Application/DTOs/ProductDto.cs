namespace CatalogService.Application.DTOs;

public sealed class ProductDto
{
    public ProductDto(Guid id, string name, string sku, decimal amount, string currency, Guid categoryId, string? description)
    {
        Id = id;
        Name = name;
        Sku = sku;
        Amount = amount;
        Currency = currency;
        CategoryId = categoryId;
        Description = description;
    }

    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty;

    public string Sku { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public string Currency { get; init; } = string.Empty;

    public Guid CategoryId { get; init; }

    public string Status { get; init; } = string.Empty;
    public decimal Amount { get; }
    public string? Description { get; }
}