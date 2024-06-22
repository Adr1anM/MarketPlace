using AutoMapper;
using MarketPlace.Application.App.ShoppingCarts.Responses;
using MarketPlace.Domain.Models;


namespace MarketPlace.Application.Profiles
{
    public class ShopppingCartProfile : Profile
    {
        public ShopppingCartProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>()
             .ForMember(dest => dest.ShoppingCartItems, opt => opt.MapFrom(src => src.ShoppingCartItems));
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>()
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Product.Title))
                .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Product.Description));
               
        }
    }
}
