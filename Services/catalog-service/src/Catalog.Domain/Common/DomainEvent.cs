using CatalogService.Domain.Abstractions;

namespace CatalogService.Domain.Common;

public abstract class DomainEvent : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();

    public DateTime OccurredOnUtc { get; } = DateTime.UtcNow;
}