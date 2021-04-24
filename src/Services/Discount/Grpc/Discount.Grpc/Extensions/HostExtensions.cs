using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Grpc.Extensions
{
    public static class HostExtensions
    {
        public static void MigrateDatabase<TContext>(this IHost host, int retry = 0)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migrating PostgreSQL database");

                using var connection =
                    new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                connection.Open();

                using var command = new NpgsqlCommand { Connection = connection };

                command.CommandText = "DROP TABLE IF EXISTS Coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductId VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon(ProductId, Description, Amount) VALUES('602d2149e773f2a3990b47f8', 'NVIDIA GeForce RTX 3080 Discount', 150);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO Coupon(ProductId, Description, Amount) VALUES('602d2149e773f2a3990b47f5', 'AMD Ryzen 7 5800X Discount', 150);";
                command.ExecuteNonQuery();

                logger.LogInformation("Successfully migrated PostgreSQL database");
            }
            catch (NpgsqlException e)
            {
                const int retryCount = 20;
                logger.LogError(e, "Error occurred while migrating PostgreSQL database" + (retry < retryCount ? ", retrying." : ""));

                if (retry < retryCount)
                {
                    retry++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(host, retry);
                }
            }
        }
    }
}