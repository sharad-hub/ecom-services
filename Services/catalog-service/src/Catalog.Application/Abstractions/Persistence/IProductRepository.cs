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

    Task UpdateAsync(Product product,
    CancellationToken cancellationToken = default);

    Task DeleteAsync(Product product,
    CancellationToken cancellationToken = default);

    Task<(List<Product>, int)> GetPagedAsync(
    int page,
    int pageSize,
    string? name,
    string? sku,
    Guid? categoryId,
    string sortBy,
    bool descending,
    CancellationToken cancellationToken = default);
}