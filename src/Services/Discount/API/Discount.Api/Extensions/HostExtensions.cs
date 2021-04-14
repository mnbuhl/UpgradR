using Microsoft.Extensions.Hosting;

namespace Discount.Api.Extensions
{
    public static class HostExtensions
    {
        public static void MigrateDatabase<TContext>(this IHost host, int? retry = 0)
        {

        }
    }
}