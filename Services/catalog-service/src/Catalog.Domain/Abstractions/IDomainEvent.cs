namespace CatalogService.Domain.Abstractions;

public interface IDomainEvent
{
    Guid EventId { get; }
    DateTime OccurredOnUtc { get; }
}