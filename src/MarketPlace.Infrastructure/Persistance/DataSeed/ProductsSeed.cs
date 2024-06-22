using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;


namespace MarketPlace.Infrastructure.DataSeed
{
    public class ProductsSeed
    {
        public static async Task Seed(ArtMarketPlaceDbContext context)
        {
            if (!context.Products.Any())
            {
                var products = new List<Product>
                {
                    new Product
                    {
                        Title = "Title 1",
                        Description = "Description 1",
                        CategoryID = 1,
                        AuthorId = 1,
                        Quantity = 10,
                        Price = 100.00m,
                        CreatedDate = new DateTime(2024, 6, 1),
                        ImageData = new byte[0]
                    },
                    new Product
                    {
                        Title = "Title 2",
                        Description = "Description 2",
                        CategoryID = 2,
                        AuthorId = 2,
                        Quantity = 20,
                        Price = 200.00m,
                        CreatedDate = new DateTime(2024, 6, 2),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 3",
                        Description = "Description 3",
                        CategoryID = 3,
                        AuthorId = 3,
                        Quantity = 30,
                        Price = 300.00m,
                        CreatedDate = new DateTime(2024, 6, 3),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 4",
                        Description = "Description 4",
                        CategoryID = 4,
                        AuthorId = 3,
                        Quantity = 40,
                        Price = 400.00m,
                        CreatedDate = new DateTime(2024, 6, 4),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 5",
                        Description = "Description 5",
                        CategoryID = 3,
                        AuthorId = 4,
                        Quantity = 50,
                        Price = 500.00m,
                        CreatedDate = new DateTime(2024, 6, 5),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 6",
                        Description = "Description 6",
                        CategoryID = 3,
                        AuthorId = 5,
                        Quantity = 60,
                        Price = 600.00m,
                        CreatedDate = new DateTime(2024, 6, 6),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 7",
                        Description = "Description 7",
                        CategoryID = 2,
                        AuthorId = 6,
                        Quantity = 70,
                        Price = 700.00m,
                        CreatedDate = new DateTime(2024, 6, 7),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 8",
                        Description = "Description 8",
                        CategoryID = 4,
                        AuthorId = 7,
                        Quantity = 80,
                        Price = 800.00m,
                        CreatedDate = new DateTime(2024, 6, 8),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 9",
                        Description = "Description 9",
                        CategoryID = 1,
                        AuthorId = 1,
                        Quantity = 90,
                        Price = 900.00m,
                        CreatedDate = new DateTime(2024, 6, 9),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 10",
                        Description = "Description 10",
                        CategoryID = 2,
                        AuthorId = 2,
                        Quantity = 100,
                        Price = 1000.00m,
                        CreatedDate = new DateTime(2024, 6, 10),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 11",
                        Description = "Description 11",
                        CategoryID = 1,
                        AuthorId = 3,
                        Quantity = 110,
                        Price = 1100.00m,
                        CreatedDate = new DateTime(2024, 6, 11),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 12",
                        Description = "Description 12",
                        CategoryID = 3,
                        AuthorId = 4,
                        Quantity = 120,
                        Price = 1200.00m,
                        CreatedDate = new DateTime(2024, 6, 12),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 13",
                        Description = "Description 13",
                        CategoryID = 4,
                        AuthorId = 5,
                        Quantity = 130,
                        Price = 1300.00m,
                        CreatedDate = new DateTime(2024, 6, 13),
                        ImageData = new byte[0] 
                    },
                    new Product
                    {
                        Title = "Title 14",
                        Description = "Description 14",
                        CategoryID = 4,
                        AuthorId = 6,
                        Quantity = 140,
                        Price = 1400.00m,
                        CreatedDate = new DateTime(2024, 6, 14),
                        ImageData = new byte[0]
                    },
                    new Product
                    {
                        Title = "Title 15",
                        Description = "Description 15",
                        CategoryID = 3,
                        AuthorId = 7,
                        Quantity = 150,
                        Price = 1500.00m,
                        CreatedDate = new DateTime(2024, 6, 15),
                        ImageData = new byte[0] 
                    }
                };

                context.Products.AddRange(products);             
                await context.SaveChangesAsync();    
            }
        }
    }
}
