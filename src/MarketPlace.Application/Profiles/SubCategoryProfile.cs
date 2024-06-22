using AutoMapper;
using MarketPlace.Application.App.Categories.Commands;
using MarketPlace.Application.App.Categories.Responses;
using MarketPlace.Domain.Models;


namespace MarketPlace.Application.Profiles
{
    public class SubCategoryProfile : Profile
    {
        public SubCategoryProfile()
        {
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
            CreateMap<CreateSubCategory, SubCategory>();
   
        }
    }
}
