using System.Collections.Generic;
using System.Threading.Tasks;
using Delivery.Models;

namespace Delivery.DataAccess;

public interface IOrdersRepository
{
    IEnumerable<OrderViewModel> FindAll();
    Task<OrderViewModel?> FindAsync(int orderNumber);
}
