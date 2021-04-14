using Dapper;
using Discount.Core.Entities;
using Discount.Core.Interfaces;
using Npgsql;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly INpgConnectionString _connectionString;

        public DiscountRepository(INpgConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public NpgsqlConnection CreateNpgScope()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<IList<Coupon>> GetAllDiscounts()
        {
            await using var connection = CreateNpgScope();

            var coupons = await connection.QueryAsync<Coupon>
                ("SELECT * FROM Coupon");

            return coupons.ToList();
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            await using var connection = CreateNpgScope();

            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });

            return coupon ?? new Coupon { ProductId = productId, Description = "No discount available", Amount = 0 };
        }

        public Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateDiscount(Coupon coupon)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteDiscount(string productId)
        {
            throw new System.NotImplementedException();
        }
    }
}