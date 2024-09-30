using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Models;

namespace Delivery.DataAccess;

public interface IOrdersRepository
{
    IAsyncEnumerable<OrderViewModel> FindAllAsync();
    Task<OrderViewModel> FindAsync(int orderNumber);
}
