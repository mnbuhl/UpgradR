using Microsoft.EntityFrameworkCore;
using Order.Application.Interfaces;
using Order.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Domain.Entities.Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Domain.Entities.Order>> GetOrdersByUsername(string userName)
        {
            var orders = await _context.Orders.Where(o => o.UserName == userName).ToListAsync();

            return orders;
        }
    }
}
