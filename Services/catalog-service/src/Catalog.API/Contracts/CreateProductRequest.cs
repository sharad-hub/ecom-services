namespace Catalog.API.Contracts;

public record CreateProductRequest(
    string Sku,
    string Name,
    decimal Price,
    Guid CategoryId,
    string Description);