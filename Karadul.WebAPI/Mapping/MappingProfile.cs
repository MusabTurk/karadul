using AutoMapper;
using Karadul.Data.Entities;
using Karadul.WebAPI.Models.AdminModels;
using Karadul.WebAPI.Models.ProductModels;


namespace Karadul.WebAPI.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Product, CreateProductModel>()
                //.ForMember(dest => dest.CategoriesId, opt => opt.MapFrom(src => src.CategoryProducts.Select(x=>x.Id)))
                .ReverseMap();
            CreateMap<Product, UpdateProductModel>().ReverseMap();
            CreateMap<Product, ProductModel>().ReverseMap();

            CreateMap<Admin, LoginModel>().ReverseMap();
            CreateMap<Admin, AdminModel>().ReverseMap();

        }
    }
}
