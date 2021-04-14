using Discount.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Core.Interfaces
{
    public interface IDiscountRepository
    {
        Task<IList<Coupon>> GetAllDiscounts();
        Task<Coupon> GetDiscount(string productId);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productId);
    }
}