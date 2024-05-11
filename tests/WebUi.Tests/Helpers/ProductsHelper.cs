using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUi.Tests.Helpers
{
    public static class ProductsHelper
    {
        public static void SeedProducts(ArtMarketPlaceDbContext context)
        {
            var products = GetSampleProducts();
            context.Products.AddRange(products);
           
        }

        public static IEnumerable<ProductDto> GetAllSeededProducts()
        {
            var products = GetSampleProducts();
            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                CategoryID = product.CategoryID,
                AuthorId = product.AuthorId,
                Quantity = product.Quantity,
                Price = product.Price,
                CreatedDate = product.CreatedDate
            }).ToList();
        }

        private static IEnumerable<Product> GetSampleProducts()
        {

            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Title = "Title",
                    Description = "Descriprion",
                    CategoryID = 2,
                    AuthorId = 3,
                    Quantity = 7,
                    Price = 3000,
                    CreatedDate = DateTime.UtcNow,
                },

                 new Product
                {
                    Id = 2,
                    Title = "Title2",
                    Description = "Descriprion2",
                    CategoryID = 2,
                    AuthorId = 3,
                    Quantity = 7,
                    Price = 20000,
                    CreatedDate = DateTime.UtcNow,
                },

                new Product
                {
                    Id = 3,
                    Title = "Title3",
                    Description = "Descriprion3",
                    CategoryID = 1,
                    AuthorId = 2,
                    Quantity = 7,
                    Price = 902,
                    CreatedDate = DateTime.UtcNow,
                },

            };
        }
    }
}
