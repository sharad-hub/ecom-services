using CatalogService.Application.Abstractions.Messaging;
using CatalogService.Application.Abstractions.Persistence;
using Catalog.Infrastructure.Messaging;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
    configuration.GetConnectionString("CatalogDb");

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("CatalogDb connection string not found");
        }
        services.AddDbContext<CatalogDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("CatalogDb"));
        });

        services.AddScoped<IProductRepository,
            ProductRepository>();

        services.AddScoped<IUnitOfWork,
            UnitOfWork>();

        services.AddScoped<IEventPublisher,
            InMemoryEventPublisher>();

        return services;
    }
}