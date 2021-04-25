using AutoMapper;
using EventBus.Messages.Events;

namespace Basket.Core.Contracts.v1.CheckoutBaskets
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>();
        }
    }
}
