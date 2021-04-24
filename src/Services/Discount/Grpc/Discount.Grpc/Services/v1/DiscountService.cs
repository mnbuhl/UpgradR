using AutoMapper;
using Discount.Grpc.Core.Entities;
using Discount.Grpc.Core.Interfaces;
using Discount.Grpc.Protos.v1;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discount.Grpc.Services.v1
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger<DiscountService> _logger;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
        {
            _discountRepository = discountRepository;
            _logger = logger;
            _mapper = mapper;
        }

        public override async Task<CouponResponse> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductId);

            if (coupon == null)
            {
                _logger.LogError(new RpcException(new Status(StatusCode.NotFound,
                    $"Error finding result for productId={request.ProductId}")),
                    $"Discount with ProductId={request.ProductId} is not found.");

                return null;
            }

            _logger.LogInformation($"Discount retrieved for ProductId: {coupon.ProductId} - Amount: {coupon.Amount}");

            var couponResponse = _mapper.Map<CouponResponse>(coupon);

            return couponResponse;
        }

        public override async Task<CouponResponseList> GetAllDiscounts(GetAllDiscountsRequest request, ServerCallContext context)
        {
            var coupons = await _discountRepository.GetAllDiscounts();

            var couponResponseList = new CouponResponseList();
            couponResponseList.CouponResponse.Add(_mapper.Map<IList<CouponResponse>>(coupons));

            return couponResponseList;
        }

        public override async Task<CouponResponse> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            bool created = await _discountRepository.CreateDiscount(coupon);

            if (!created)
            {
                _logger.LogError(new RpcException(new Status(StatusCode.InvalidArgument,
                        $"Error creating discount for {request.Coupon.ProductId}")),
                    $"Discount with ProductId={request.Coupon.ProductId} has not been created.");

                return null;
            }

            _logger.LogInformation($"Discount successfully created for ProductId: " +
                                   $"{coupon.ProductId} - Amount: {coupon.Amount} - Description: {coupon.Description}");

            var couponResponse = _mapper.Map<CouponResponse>(coupon);
            return couponResponse;
        }

        public override async Task<CouponResponse> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            bool updated = await _discountRepository.UpdateDiscount(coupon);

            if (!updated)
            {
                _logger.LogError(new RpcException(new Status(StatusCode.NotFound,
                        $"Error finding result for productId={request.Coupon.ProductId}")),
                    $"Discount with ProductId={request.Coupon.ProductId} has not been found.");

                return null;
            }

            _logger.LogInformation($"Discount with ProductId: {coupon.Id} has been successfully updated");

            var couponResponse = _mapper.Map<CouponResponse>(coupon);
            return couponResponse;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            bool deleted = await _discountRepository.DeleteDiscount(request.ProductId);

            if (!deleted)
            {
                _logger.LogError(new RpcException(new Status(StatusCode.NotFound,
                        $"Error finding result for productId={request.ProductId}")),
                    $"Discount with ProductId={request.ProductId} has not been found.");
            }

            var response = new DeleteDiscountResponse { Success = deleted };

            return response;
        }
    }
}