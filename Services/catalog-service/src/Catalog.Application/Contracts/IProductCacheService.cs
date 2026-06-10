using CatalogService.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain.Contract
{
    public interface IProductCacheService
    {
        Task<ProductDto?> GetAsync(Guid id);

        Task SetAsync(ProductDto product);

        Task RemoveAsync(Guid id);
    }
}
