using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.DeleteProduct
{
    public record DeleteProductCommand(
     Guid Id)
     : IRequest;
}
