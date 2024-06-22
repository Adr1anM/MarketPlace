using AutoMapper;
using MarketPlace.Application.App.Categories.Commands;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Domain;
using MarketPlace.Domain.Models;


namespace MarketPlace.Application.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategory, Category>();
            CreateMap<UpdateCategory, Category>();
            
        }
    }
}
