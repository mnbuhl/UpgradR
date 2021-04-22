using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infrastructure.Data
{
    public class OrderContextSeed
    {
        public static async Task SeedAsync(OrderContext context, ILogger<OrderContextSeed> logger)
        {
            if (context.Orders.Any())
                return;

            await context.Orders.AddRangeAsync(new Domain.Entities.Order
            {
                UserName = "Zorah",
                FirstName = "Mikkel",
                LastName = "Buhl",
                EmailAddress = "fake@gmail.com",
                AddressLine = "Somewhere 112",
                Country = "Denmark",
                TotalPrice = 350
            });

            await context.SaveChangesAsync();
            logger.LogInformation("Successfully seeded the database");
        }
    }
}