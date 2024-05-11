using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUi.Tests.Helpers
{
    public static class OrdersHelper
    {
        public static void SeedOrders(ArtMarketPlaceDbContext context)
        {
            var orders = GetSampleOrders();
            context.Orders.AddRange(orders);
        }

        public static IEnumerable<OrderDto> GetAllSeededOrders()
        {
            var orders = GetSampleOrders();
            return orders.Select(order => new OrderDto
            {
                Id = order.Id,
                CretedById = order.CretedById,
                CreatedDate = order.CreatedDate,
                PromocodeId = order.PromocodeId,
                Quantity = order.Quantity,
                ShippingAdress = order.ShippingAdress,
                StatusId = order.StatusId,
                TotalPrice = order.TotalPrice
            }).ToList();
        }
        public static IEnumerable<Order> GetSampleOrders()
        {

            var user = new User { Id = 1, UserName = "TestUser" };
            var promocode = new Promocode { Id = 1, Code = "DISCOUNT10" };

            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CretedById = user.Id,
                    CreatedDate = DateTime.Now,
                    PromocodeId = promocode.Id,
                    Quantity = 2,
                    ShippingAdress = "123 Main St, Anytown USA",
                    StatusId = OrderStatus.Processing.Id,
                    TotalPrice = 49.99m
                },
                new Order
                {
                    Id = 2,
                    CretedById = user.Id,
                    CreatedDate = DateTime.Now.AddDays(-7),
                    PromocodeId = 2,
                    Quantity = 1,
                    ShippingAdress = "456 Oak Rd, Elsewhere USA",
                    StatusId = OrderStatus.Shipped.Id,
                    TotalPrice = 29.99m
                },
                new Order
                {
                    Id = 3,
                    CretedById = user.Id,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    PromocodeId = 2,
                    Quantity = 3,
                    ShippingAdress = "789 Maple Ave, Someplace USA",
                    StatusId = OrderStatus.Delivered.Id,
                    TotalPrice = 99.99m
                },

            };


        }
    }
}
