using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.Commands.UpdateProduct
{
   
    public record UpdateProductCommand(
    Guid Id,
    string Name,
    decimal Price,
    string Currency,
    string? Description)
    : IRequest;
}
