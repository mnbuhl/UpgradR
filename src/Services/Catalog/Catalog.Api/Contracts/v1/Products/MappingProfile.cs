using AutoMapper;
using Catalog.Api.Entities;

namespace Catalog.Api.Contracts.v1.Products
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