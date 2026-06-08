using CatalogService.Application.DTOs;
using MediatR;

namespace CatalogService.Application.Products.Queries.GetProducts;

public record GetProductsQuery()
    : IRequest<List<ProductDto>>;
