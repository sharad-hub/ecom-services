using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infrastructure.Persistence.Repositories;

public sealed class ProductRepository
    : IProductRepository
{
    private readonly CatalogDbContext _context;

    public ProductRepository(
        CatalogDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Product product,
        CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(
            product,
            cancellationToken);
    }

    public async Task<Product?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<bool> ExistsBySkuAsync(
        string sku,
        CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(
                x => x.Sku   == sku,
                cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(
    CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}