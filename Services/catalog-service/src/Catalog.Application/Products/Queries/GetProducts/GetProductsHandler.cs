using Catalog.Application.Products.Queries.GetProductById;
using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Application.DTOs;
using MediatR;

namespace CatalogService.Application.Products.Queries.GetProducts;

using MediatR;

 

public sealed class GetProductsHandler
    : IRequestHandler<GetProductsQuery, PagedResponse<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetProductsHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResponse<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var (products, totalCount) =
            await _repository.GetPagedAsync(
                request.Page,
                request.PageSize,
                request.Name,
                request.Sku,
                cancellationToken);

        var items = products
            .Select(product => new ProductDto(
                product.Id,
                product.Name,
                product.Sku,                
                product.Price.Amount,
                product.Price.Currency,
               Guid.Parse("11111111-1111-1111-1111-111111111111"),
                product.Description))
            .ToList();

        return new PagedResponse<ProductDto>(
            items,
            request.Page,
            request.PageSize,
            totalCount);
    }
}
