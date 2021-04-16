using Dapper;
using Discount.Grpc.Core.Entities;
using Discount.Grpc.Core.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly INpgConnectionString _connectionString;

        public DiscountRepository(INpgConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection CreateNpgConnectionScope()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IList<Coupon>> GetAllDiscounts()
        {
            await using var connection = CreateNpgConnectionScope();

            var coupons = await connection.QueryAsync<Coupon>
                ("SELECT * FROM Coupon");

            return coupons.ToList();
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            await using var connection = CreateNpgConnectionScope();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });

            return coupon ?? new Coupon { ProductId = productId, Description = "No discount available", Amount = 0 };
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            await using var connection = CreateNpgConnectionScope();

            bool success = await connection.ExecuteAsync
                ("INSERT INTO Coupon (ProductId, Description, Amount) VALUES (@ProductId, @Description, @Amount)",
                new { coupon.ProductId, coupon.Description, coupon.Amount }) > 0;

            return success;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            await using var connection = CreateNpgConnectionScope();

            bool success = await connection.ExecuteAsync
                ("UPDATE Coupon SET ProductId = @ProductId, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new { coupon.ProductId, coupon.Description, coupon.Amount, coupon.Id }) > 0;

            return success;
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            await using var connection = CreateNpgConnectionScope();

            bool success = await connection.ExecuteAsync
                ("DELETE FROM Coupon WHERE ProductId = @ProductId",
                new { ProductId = productId }) > 0;

            return success;
        }
    }
}