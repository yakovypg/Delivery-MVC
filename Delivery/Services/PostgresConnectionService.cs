using Delivery.DataAccess;

namespace Delivery.Services;

public static class PostgresConnectionService
{
    static PostgresConnectionService()
    {
        OrdersHandler = new OrdersHandler();
        OrdersRepository = new OrdersRepository();
    }

    public static IOrdersHandler OrdersHandler { get; }
    public static IOrdersRepository OrdersRepository { get; }
}
