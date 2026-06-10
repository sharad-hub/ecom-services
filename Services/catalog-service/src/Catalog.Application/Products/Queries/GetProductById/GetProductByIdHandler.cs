using Catalog.Domain.Contract;
using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Application.DTOs;
using MediatR;

namespace CatalogService.Application.Products.Queries.GetProductById;

public sealed class GetProductByIdHandler
    : IRequestHandler<GetProductByIdQuery, ProductDto?>
{
    private readonly IProductRepository _repository;
    private readonly IProductCacheService _cacheService;

    public GetProductByIdHandler(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductDto?> Handle(
        GetProductByIdQuery request,
        CancellationToken cancellationToken)
    {
        var cachedProduct =
    await _cacheService.GetAsync(
        request.Id);

        if (cachedProduct is not null)
        {
            return cachedProduct;
        }

        var product =
            await _repository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (product is null)
            return null;
  

       var dto= new ProductDto(
            product.Id,
            product.Name,
            product.Sku,
            product.Price.Amount,
            product.Price.Currency,
            product.CategoryId,
            product.Status.ToString()
        );

        await _cacheService.SetAsync(dto);

        return dto;
    }
}