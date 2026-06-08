using CatalogService.Domain.Aggregates;

namespace CatalogService.Application.Abstractions.Persistence;

public interface IProductRepository
{
    Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default);

    Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsBySkuAsync(
        string sku,
        CancellationToken cancellationToken = default);
    Task<List<Product>> GetAllAsync(
    CancellationToken cancellationToken = default);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);
}