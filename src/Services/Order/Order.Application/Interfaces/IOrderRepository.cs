using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Application.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Domain.Entities.Order>
    {
        Task<IEnumerable<Domain.Entities.Order>> GetOrdersByUsername(string userName);
    }
}