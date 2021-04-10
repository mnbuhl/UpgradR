using AutoMapper;
using Catalog.Core.Entities;

namespace Catalog.Core.Contracts.v1.Products
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ProductCommandDto, Product>();
            CreateMap<Product, ProductResponseDto>();
        }
    }
}