using Microsoft.Extensions.DependencyInjection;
using Order.Application.Orders.v1;

namespace Order.Application
{
    public static class ServiceExtensions
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile).Assembly);
        }
    }
}
