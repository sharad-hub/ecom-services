using Catalog.Domain.Contract;
using CatalogService.Application.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Persistence
{
    public sealed class ProductCacheService
    : IProductCacheService
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger<ProductCacheService> _logger;

        public ProductCacheService(
            IDistributedCache cache,ILogger<ProductCacheService> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<ProductDto?> GetAsync(Guid id)
        {
            var json =
                await _cache.GetStringAsync(
                    $"product:{id}");
            _logger.LogInformation("Cache hit for product {Id}",id);

            return json is null
                ? null
                : JsonSerializer.Deserialize<ProductDto>(json);
        }

        public async Task SetAsync(ProductDto product)
        {
            await _cache.SetStringAsync(
                $"product:{product.Id}",
                JsonSerializer.Serialize(product),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromMinutes(10)
                });
        }

        public async Task RemoveAsync(Guid id)
        {
            await _cache.RemoveAsync(
                $"product:{id}");
        }
    }
}
