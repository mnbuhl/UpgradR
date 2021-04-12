using AutoMapper;
using Basket.Core.Entities;

namespace Basket.Core.Contracts.v1.ShoppingCarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ShoppingCartCommandDto, ShoppingCart>();
            CreateMap<ShoppingCart, ShoppingCartResponseDto>();
        }
    }
}
