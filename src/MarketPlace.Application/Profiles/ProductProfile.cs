using AutoMapper;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Common.Helpers.AutomapperHelpers;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Application.Products.Delete;
using MarketPlace.Domain.Models;


namespace MarketPlace.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Author.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Author.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Author.User.LastName))
            .ForMember(dest => dest.ImageData, opt => opt.MapFrom(src => src.ImageData != null ? ConvertToBase64String.Convevrt(src.ImageData) : null))
            .ForMember(dest => dest.SubCategoryIds, opt => opt.MapFrom(src => src.ProductSubcategories.Select(ps => ps.SubCategoryId).ToList()));

            CreateMap<UpdateProduct, Product>()
                    .ForMember(dest => dest.ProductSubcategories, opt => opt.Ignore())
                    .ForMember(dest => dest.ImageData, opt => opt.ConvertUsing(new FormFileByteArrayValueConverter(), src => src.ImageData))
                    .ReverseMap();
            CreateMap<CreateProduct, Product>()
                    .ForMember(dest => dest.ImageData, opt => opt.ConvertUsing(new FormFileByteArrayValueConverter(), src => src.ImageData))
                    .ReverseMap();

            CreateMap<DeleteProduct, Product>().ReverseMap();
            
        }   
    }
}
