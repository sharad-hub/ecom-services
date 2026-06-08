using CatalogService.Application.Abstractions.Messaging;
using CatalogService.Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.Messaging;

public sealed class InMemoryEventPublisher
    : IEventPublisher
{
    private readonly ILogger<InMemoryEventPublisher>
        _logger;

    public InMemoryEventPublisher(
        ILogger<InMemoryEventPublisher> logger)
    {
        _logger = logger;
    }

    public Task PublishAsync(
        IReadOnlyCollection<IDomainEvent> domainEvents,
        CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in domainEvents)
        {
            _logger.LogInformation(
                "Domain Event Published: {Event}",
                domainEvent.GetType().Name);
        }

        return Task.CompletedTask;
    }
}