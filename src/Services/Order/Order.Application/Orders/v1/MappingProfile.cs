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
            CreateMap<CheckoutCommand, Domain.Entities.Order>();
            CreateMap<UpdateCommand, Domain.Entities.Order>();
            CreateMap<Domain.Entities.Order, OrderResponseDto>();
        }
    }
}