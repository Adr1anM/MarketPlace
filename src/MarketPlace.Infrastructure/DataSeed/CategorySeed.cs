using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;


namespace MarketPlace.Infrastructure.DataSeed
{
    public class CategorySeed
    {
        public static async Task Seed(ArtMarketPlaceDbContext context)
        {
            if(!context.Categories.Any()) 
            {
                var categories = new List<Category>
                {
                    new Category
                    {                  
                        Name = "Paint"
                    },
                    new Category
                    {
                        Name = "Sculpture"
                    },
                    new Category
                    {
                        Name = "Drawing"
                    },
                    new Category
                    {
                        Name = "Photography"
                    },
                    new Category
                    {
                        Name = "Ceramics"
                    }
                };

                context.Categories.AddRange(categories); 
                await context.SaveChangesAsync();
            }
        }
    }
}
