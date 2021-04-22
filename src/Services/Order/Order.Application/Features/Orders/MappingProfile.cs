using AutoMapper;
using Order.Application.Features.Orders.Commands;
using Order.Application.Features.Orders.Queries;

namespace Order.Application.Features.Orders
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