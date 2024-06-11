using AutoMapper;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Application.Products.Delete;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
            .ForMember(dest => dest.AuthorId, opt => opt.MapFrom(src => src.AuthorId))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.CreatedDate))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Author.UserId))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Author.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Author.User.LastName));

            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdateProduct, Product>().ReverseMap();
            CreateMap<DeleteProduct, Product>().ReverseMap();
            
        }
    }
}
