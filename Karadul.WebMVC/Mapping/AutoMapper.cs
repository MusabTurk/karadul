using AutoMapper;
using Karadul.Data.Entities;
using Karadul.WebMVC.Dtos.LoginDtos;
using Karadul.WebMVC.Dtos.ProductDtos;

namespace Karadul.WebMVC.Mapping
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            #region Product
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, CreateProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductDto>().ReverseMap();

            #endregion


            #region Auth

            CreateMap<Admin, LoginDto>().ReverseMap();

            #endregion

        }
    }
}
