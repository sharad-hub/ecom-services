using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

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

    public async Task UpdateAsync(
    Product product,
    CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);

        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Product product,
    CancellationToken cancellationToken = default)
    {
        //_context.Products.Remove(product);
        //await Task.CompletedTask;
    }

    public async Task<(List<Product>, int)> GetPagedAsync(
    int page,
    int pageSize,
    string? name,
    string? sku,
    Guid? categoryId,
    string sortBy,
    bool descending,
    CancellationToken cancellationToken = default)
    {
        IQueryable<Product> query =
            _context.Products
                .AsNoTracking();

        if (!string.IsNullOrWhiteSpace(name))
        {
            query = query.Where(
                x => x.Name.Contains(name));
        }

        if (!string.IsNullOrWhiteSpace(sku))
        {
            query = query.Where(
                x => x.Sku.Contains(sku));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(
                x => x.CategoryId == categoryId.Value);
        }

        query = sortBy.ToLower() switch
        {
            "price" => descending
                ? query.OrderByDescending(x => x.Price.Amount)
                : query.OrderBy(x => x.Price.Amount),

            "createdon" => descending
                ? query.OrderByDescending(x => x.CreatedOnUtc)
                : query.OrderBy(x => x.CreatedOnUtc),

            _ => descending
                ? query.OrderByDescending(x => x.Name)
                : query.OrderBy(x => x.Name)
        };

        int totalCount =
            await query.CountAsync(cancellationToken);

        var products =
            await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

        return (products, totalCount);
    }
}