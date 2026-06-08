using CatalogService.Domain.Abstractions;

namespace CatalogService.Application.Abstractions.Messaging;

public interface IEventPublisher
{
    Task PublishAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default);
}