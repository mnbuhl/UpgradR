using AutoMapper;
using Discount.Grpc.Core.Entities;
using Discount.Grpc.Protos.v1;

namespace Discount.Grpc.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateCoupon, Coupon>();
            CreateMap<UpdateCoupon, Coupon>();
            CreateMap<Coupon, CouponResponse>();
        }
    }
}