using CatalogService.Domain.Common;

namespace CatalogService.Domain.Events;

public sealed class ProductCreatedEvent : DomainEvent
{
    public Guid ProductId { get; }

    public string Sku { get; }

    public ProductCreatedEvent(
        Guid productId,
        string sku)
    {
        ProductId = productId;
        Sku = sku;
    }
}