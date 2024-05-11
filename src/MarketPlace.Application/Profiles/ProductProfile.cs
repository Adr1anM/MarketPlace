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
            CreateMap<Product,ProductDto>().ReverseMap();
            CreateMap<CreateProduct, Product>().ReverseMap();
            CreateMap<UpdatePruduct, Product>().ReverseMap();
            CreateMap<DeleteProduct, Product>().ReverseMap();
            
        }
    }
}
