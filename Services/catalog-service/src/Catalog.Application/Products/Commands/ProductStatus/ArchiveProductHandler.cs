using CatalogService.Application.Abstractions.Messaging;
using CatalogService.Application.Abstractions.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Products.Commands.ProductStatus
{
    public sealed class ArchiveProductHandler : IRequestHandler<ArchiveProductCommand>
    {
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger<ArchiveProductHandler> _logger;
        public ArchiveProductHandler(IProductRepository repository, IUnitOfWork unitOfWork, IEventPublisher eventPublisher, ILogger<ArchiveProductHandler> logger)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public async Task Handle(
    ArchiveProductCommand request,
    CancellationToken cancellationToken)
        {
            var product =
                await _repository.GetByIdAsync(
                    request.Id,
                    cancellationToken);

            if (product is null)
                throw new Exception("Product not found");

            product.Archive();

            await _unitOfWork.SaveChangesAsync(
                cancellationToken);
        }
    }
}
