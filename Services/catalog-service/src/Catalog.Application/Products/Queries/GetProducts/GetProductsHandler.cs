using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Application.DTOs;
using MediatR;

namespace CatalogService.Application.Products.Queries.GetProducts;

public sealed class GetProductsHandler
    : IRequestHandler<GetProductsQuery, List<ProductDto>>
{
    private readonly IProductRepository _repository;

    public GetProductsHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> Handle(
        GetProductsQuery request,
        CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync(
            cancellationToken);

        return products.Select(x =>
            new ProductDto(
                x.Id,
                x.Name,
                x.Sku,                
                x.Price.Amount,
                x.Price.Currency,
                x.CategoryId,
                x.Description))
            .ToList();
    }
}