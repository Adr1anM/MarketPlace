using AutoMapper;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Application.Orders.Update;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.Application.Profiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order,OrderDto>().ReverseMap();
            CreateMap<CreateOrder, Order>().ReverseMap();
            CreateMap<UpdateOrder, Order>().ReverseMap();
            CreateMap<DeleteOrder, Order>().ReverseMap();

        }
    }
}
