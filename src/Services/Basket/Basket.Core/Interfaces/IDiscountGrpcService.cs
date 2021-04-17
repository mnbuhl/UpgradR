using Discount.Grpc.Protos.v1;
using System.Threading.Tasks;

namespace Basket.Core.Interfaces
{
    public interface IDiscountGrpcService
    {
        Task<CouponResponse> GetDiscount(string productId);
    }
}