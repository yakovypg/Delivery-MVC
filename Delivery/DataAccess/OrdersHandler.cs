using System;
using System.Linq;
using System.Threading.Tasks;
using Delivery.Models;
using Microsoft.EntityFrameworkCore;

namespace Delivery.DataAccess;

public class OrdersHandler : IOrdersHandler
{
    public OrdersHandler() { }

    public async Task AddAsync(OrderViewModel order)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));

        order.Number = await GetNextOrderNumber().ConfigureAwait(false);

        using var db = new ApplicationContext();

        await db.Orders
            .AddAsync(order)
            .ConfigureAwait(false);

        await db
            .SaveChangesAsync()
            .ConfigureAwait(false);
    }

    private static async Task<int> GetNextOrderNumber()
    {
        using var db = new ApplicationContext();

        bool hasOrders = await db.Orders
            .AnyAsync()
            .ConfigureAwait(false);

        int orderNumber = 0;

        if (hasOrders)
        {
            orderNumber = await db.Orders
                .MaxAsync(t => t.Number)
                .ConfigureAwait(false);
        }

        return orderNumber + 1;
    }
}
