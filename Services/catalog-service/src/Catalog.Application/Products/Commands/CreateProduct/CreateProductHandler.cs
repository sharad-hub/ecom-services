using CatalogService.Application.Abstractions.Messaging;
using CatalogService.Application.Abstractions.Persistence;
using CatalogService.Domain.Aggregates;
using CatalogService.Domain.ValueObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CatalogService.Application.Products.Commands.CreateProduct;

public sealed class CreateProductHandler
    : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<CreateProductHandler> _logger;
    public CreateProductHandler(
        IProductRepository repository,
        IUnitOfWork unitOfWork,
        IEventPublisher eventPublisher, ILogger<CreateProductHandler> logger)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    public async Task<Guid> Handle(
        CreateProductCommand request,
        CancellationToken cancellationToken)
    {
        var exists =
            await _repository.ExistsBySkuAsync(
                request.Sku,
                cancellationToken);

        if (exists)
        {
            throw new ApplicationException(
                $"SKU {request.Sku} already exists.");
        }

        var sku = Sku.Create(request.Sku).Value;

        //var sku =
        //    Sku.Create(request.Sku).Value;

        var money =
            Money.Create(
                request.Price,
                request.Currency).Value;
        _logger.LogInformation(
    "Creating product {Sku}",
    request.Sku);
        var productResult = Product.Create(
            request.Name,
            request.Sku,
            money,
            request.CategoryId,
            request.Description);
        //var productResult =
        //    Product.Create(
        //        request.Name,
        //        sku,
        //        money,
        //        request.CategoryId,
        //        request.Description);

        var product = productResult.Value;

        await _repository.AddAsync(
            product,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        await _eventPublisher.PublishAsync(
            product.DomainEvents,
            cancellationToken);

        return product.Id;
    }
}