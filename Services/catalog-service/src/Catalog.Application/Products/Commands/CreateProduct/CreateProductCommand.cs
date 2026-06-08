using MediatR;

namespace CatalogService.Application.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Sku,
    decimal Price,
    string Currency,
    Guid CategoryId,
    string? Description)
    : IRequest<Guid>;