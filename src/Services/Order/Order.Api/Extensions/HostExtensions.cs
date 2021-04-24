using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Order.Infrastructure.Data;
using System;

namespace Order.Api.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder,
            int retry = 0) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<TContext>();
                var logger = services.GetRequiredService<ILogger<OrderContextSeed>>();

                try
                {
                    logger.LogInformation($"Migrating database for {nameof(TContext)}");

                    InvokeSeeder(seeder, context, services);

                    logger.LogInformation($"Successfully migrated database for {nameof(TContext)}");
                }
                catch (Exception e)
                {
                    logger.LogError(e,
                        $"An error occurred while migrating database for {nameof(TContext)}" + (retry < 8 ? ", retrying..." : "."));

                    if (retry < 20)
                    {
                        retry++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retry);
                    }
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
            IServiceProvider services) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, services);
        }
    }
}