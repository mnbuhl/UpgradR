using Basket.Core.Interfaces;
using Discount.Grpc.Protos.v1;
using System.Threading.Tasks;

namespace Basket.Infrastructure.GrpcServices
{
    public class DiscountGrpcService : IDiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountProtoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountProtoService)
        {
            _discountProtoService = discountProtoService;
        }

        public async Task<CouponResponse> GetDiscount(string productId)
        {
            var discountRequest = new GetDiscountRequest { ProductId = productId };

            return await _discountProtoService.GetDiscountAsync(discountRequest);
        }
    }
}