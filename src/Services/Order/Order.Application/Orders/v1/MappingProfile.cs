using AutoMapper;
using Order.Application.Orders.v1.Commands.Checkout;
using Order.Application.Orders.v1.Commands.Update;
using Order.Application.Orders.v1.Queries;

namespace Order.Application.Orders.v1
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CheckoutCommand, Domain.Entities.Order>().ReverseMap();
            CreateMap<UpdateCommand, Domain.Entities.Order>().ReverseMap();
            CreateMap<Domain.Entities.Order, OrderResponseDto>().ReverseMap();
        }
    }
}