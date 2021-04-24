using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Interfaces;
using Order.Application.Models;
using Order.Infrastructure.Data;
using Order.Infrastructure.Email;
using Order.Infrastructure.Repositories;

namespace Order.Infrastructure
{
    public static class InfrastructureServiceExtensions
    {
        public static void RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.Configure<EmailSettings>(_ => configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();
        }
    }
}
