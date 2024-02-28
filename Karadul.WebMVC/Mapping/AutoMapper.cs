using AutoMapper;
using Karadul.Data.Entities;
using Karadul.WebMVC.Dtos.ProductDtos;

namespace Karadul.WebMVC.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();
        }
    }
}
