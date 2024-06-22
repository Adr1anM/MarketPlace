using AutoMapper;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.Common.Helpers;
using MarketPlace.Application.Common.Helpers.AutomapperHelpers;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class AuthorProfile : Profile
    {
        public AuthorProfile()
        {
            CreateMap<CreateAuthor, Author>()
                 .ForMember(dest => dest.ProfileImage, opt => opt.ConvertUsing(new FormFileByteArrayValueConverter(), src => src.ProfileImage));

            CreateMap<UpdateAuthor, Author>()
                .ForMember(dest => dest.ProfileImage, opt => opt.ConvertUsing(new FormFileByteArrayValueConverter(), src => src.ProfileImage));
             
            CreateMap<DeleteAuthor, Author>().ReverseMap();

            CreateMap<Author, AuthorDto>()
                    .ForMember(dest => dest.ProfileImage, opt => opt.ConvertUsing(new ByteArrayToBase64StringConverter(), src => src.ProfileImage))
                    .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
                    .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
                    .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email));
                    

        }
    }
}
