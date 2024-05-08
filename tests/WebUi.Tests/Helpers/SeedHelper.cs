﻿using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUi.Tests.Helpers
{
    public class SeedHelper
    {
        public static void SeedOrders(ArtMarketPlaceDbContext context)
        {

            var user = new User { Id = 1, UserName = "TestUser" };
            var promocode = new Promocode { Id = 1, Code = "DISCOUNT10" };



            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    CretedById = user.Id,
                    Buyer = user,
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
                    Buyer = user,
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
                    Buyer = user,
                    CreatedDate = DateTime.Now.AddMonths(-2),
                    PromocodeId = 2,
                    Quantity = 3,
                    ShippingAdress = "789 Maple Ave, Someplace USA",
                    StatusId = OrderStatus.Delivered.Id,
                    TotalPrice = 99.99m
                },
                new Order
                {
                    Id = 4,
                    CretedById = user.Id,
                    Buyer = user,
                    CreatedDate = DateTime.Now.AddYears(-1),
                    PromocodeId = 2,
                    Quantity = 5,
                    ShippingAdress = "321 Pine St, Anothertown USA",
                    StatusId = OrderStatus.Canceled.Id,
                    TotalPrice = 149.99m
                }
            };

            context.Orders.AddRange(orders);
            context.SaveChanges();
        }


        public static void CleanDatabase(ArtMarketPlaceDbContext context)
        {

            context.ChangeTracker
            .Entries()
            .ToList()
            .ForEach(e => e.State = EntityState.Detached);

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        }
    }
}