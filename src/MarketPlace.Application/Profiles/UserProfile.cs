using AutoMapper;
using MarketPlace.Application.App.Users.Responses;
using MarketPlace.Domain.Models.Auth;


namespace MarketPlace.Application.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() 
        {
            CreateMap<User, UserDto>().ReverseMap();
        }    
    }
}
