using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.App.Authors.Responses;

namespace WebUi.Tests.Helpers
{
    public class SeedHelper
    {

        public static void SeedAuthors(ArtMarketPlaceDbContext context)
        {
            var authors = GetSampleAuthors();
            context.Authors.AddRange(authors);
            context.SaveChanges();
        }

        public static void SeedOrders(ArtMarketPlaceDbContext context)
        {
            var orders = GetSampleOrders();
            context.Orders.AddRange(orders);
            context.SaveChanges();
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


        public static IEnumerable<AuthorDto> GetAllSeededAuthors()
        {
            var auhtors = GetSampleAuthors();
            return auhtors.Select(auhtor => new AuthorDto
            {
                Id = auhtor.Id,
                UserId = auhtor.UserId,
                Biography = auhtor.Biography,
                BirthDate = auhtor.BirthDate,
                SocialMediaLinks = auhtor.SocialMediaLinks,
                NumberOfPosts = auhtor.NumberOfPosts,
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


        private static IEnumerable<Author> GetSampleAuthors()
        {
            return new List<Author>
            {
                new Author
                {
                    Id = 1,
                    UserId = 2,
                    Biography = "Biography",
                    Country = "Country",
                    BirthDate = DateTime.Now,
                    SocialMediaLinks = "SocialMediaLinks",
                    NumberOfPosts = 3
                },

                new Author
                {
                    Id = 2,
                    UserId = 2,
                    Biography = "Biography2",
                    Country = "Germany",
                    BirthDate = DateTime.Now,
                    SocialMediaLinks = "SocialMedia2",
                    NumberOfPosts = 2
                },
                new Author
                {
                    Id = 3,
                    UserId = 4,
                    Biography = "Biography3",
                    Country = "France",
                    BirthDate = DateTime.Now,
                    SocialMediaLinks = "SocialMedia3",
                    NumberOfPosts = 9
                },

            };
        }
    

        public static void SeedDatabase(ArtMarketPlaceDbContext context)
        {
            OrdersHelper.SeedOrders(context);
            ProductsHelper.SeedProducts(context);
            AuthorsHelper.SeedAuthors(context);
            CategoriesHelper.SeedCategories(context);
            SeedUsers(context);
            context.SaveChanges();
        }

        public static void SeedUsers(ArtMarketPlaceDbContext context)
        {
            var users = new List<User>
            {
                new User { Id = 1, UserName = "user1@example.com", Email = "user1@example.com" },
                new User { Id = 2, UserName = "user2@example.com", Email = "user2@example.com" },

            };

            context.Users.AddRange(users);
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
