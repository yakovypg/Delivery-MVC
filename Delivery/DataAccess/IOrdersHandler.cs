using System.Threading.Tasks;
using Delivery.Models;

namespace Delivery.DataAccess;

public interface IOrdersHandler
{
    Task AddAsync(OrderViewModel order);
}
