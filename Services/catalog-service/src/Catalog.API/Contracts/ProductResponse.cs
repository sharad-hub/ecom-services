namespace Catalog.API.Contracts;

public record ProductResponse(
    Guid Id,
    string Sku,
    string Name,
    decimal Price,
    string Description);