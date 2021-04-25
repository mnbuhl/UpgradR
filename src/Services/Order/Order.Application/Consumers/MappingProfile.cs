using AutoMapper;
using EventBus.Messages.Events;
using Order.Application.Orders.v1.Commands.Checkout;

namespace Order.Application.Consumers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CheckoutCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
