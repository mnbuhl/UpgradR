using AutoMapper;
using Discount.Core.Entities;

namespace Discount.Core.Contracts.v1.Coupons
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCouponDto, Coupon>();
            CreateMap<UpdateCouponDto, Coupon>();
            CreateMap<Coupon, CouponResponseDto>();
        }
    }
}