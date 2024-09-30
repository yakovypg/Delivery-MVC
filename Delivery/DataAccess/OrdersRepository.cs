using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Models;

namespace Delivery.DataAccess;

public class OrdersRepository : IOrdersRepository
{
    public OrdersRepository() { }

    public async Task<OrderViewModel?> FindAsync(int orderNumber)
    {
        using var db = new ApplicationContext();

        return await db.Orders
            .FindAsync(orderNumber)
            .ConfigureAwait(false);
    }

    public IEnumerable<OrderViewModel> FindAll()
    {
        using var db = new ApplicationContext();

        foreach (OrderViewModel order in db.Orders)
        {
            yield return order;
        }
    }
}
