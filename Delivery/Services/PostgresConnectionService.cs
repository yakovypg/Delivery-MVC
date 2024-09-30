using Delivery.DataAccess;
using Delivery.DataAccess.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Delivery.Services;

public static class PostgresConnectionService
{
    static PostgresConnectionService()
    {
        var serviceCollection = new ServiceCollection().AddDataAccess(t =>
        {
            t.Username = "postgres";
            t.Password = "postgres";
            t.Database = "delivery";
            t.SslMode = "Prefer";

            t.Port = 5432;
            t.Host = "localhost";
        });

        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

        using IServiceScope scope = serviceProvider.CreateScope();
        scope.UseDataAccessAsync().Wait();

        OrdersHandler = scope.ServiceProvider
            .GetRequiredService<IOrdersHandler>();

        OrdersRepository = scope.ServiceProvider
            .GetRequiredService<IOrdersRepository>();
    }

    public static IOrdersHandler OrdersHandler { get; }
    public static IOrdersRepository OrdersRepository { get; }
}
