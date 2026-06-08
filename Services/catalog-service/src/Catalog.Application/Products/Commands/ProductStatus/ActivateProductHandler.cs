using CatalogService.Application.Abstractions.Messaging;
using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Application.Products.Commands.CreateProduct;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.Commands.ProductStatus
{
  
       
        public sealed class ActivateProductHandler : IRequestHandler<ActivateProductCommand>
    {
            private readonly IProductRepository _repository;
            private readonly IUnitOfWork _unitOfWork;
            private readonly IEventPublisher _eventPublisher;
            private readonly ILogger<ActivateProductHandler> _logger;
     

        public ActivateProductHandler(IProductRepository repository, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, ILogger<ActivateProductHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public async Task Handle(
    ActivateProductCommand request,
    CancellationToken cancellationToken)
        {
            var product =
                await _repository.GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (product is null)
                throw new Exception("Product not found");

            product.Activate();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
