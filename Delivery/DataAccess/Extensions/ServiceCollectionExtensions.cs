using System;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccess(
        this IServiceCollection services,
        Action<PostgresConnectionConfiguration> configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        _ = services.AddPlatformPostgres(t => t.Configure(configuration))
            .AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly)
            .AddScoped<IOrdersHandler, OrdersHandler>()
            .AddScoped<IOrdersRepository, OrdersRepository>();

        return services;
    }
}
