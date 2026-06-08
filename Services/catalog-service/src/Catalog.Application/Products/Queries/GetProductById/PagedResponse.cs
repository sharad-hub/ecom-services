using CatalogService.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.Queries.GetProductById
{
    public sealed record PagedResponse<T>
 (
     IReadOnlyList<T> Items,
     int Page,
     int PageSize,
     int TotalCount
 );
}
