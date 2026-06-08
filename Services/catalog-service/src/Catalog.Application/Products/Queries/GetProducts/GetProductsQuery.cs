using Catalog.Application.Products.Queries.GetProductById;
using CatalogService.Application.DTOs;
using MediatR;

namespace CatalogService.Application.Products.Queries.GetProducts;

//public record GetProductsQuery()
//    : IRequest<List<ProductDto>>;
public record GetProductsQuery(
    int Page = 1,
    int PageSize = 20,
    string? Name = null,
    string? Sku = null,
    Guid? CategoryId = null,
    string SortBy = "Name",
    bool Descending = false)
    : IRequest<PagedResponse<ProductDto>>;
