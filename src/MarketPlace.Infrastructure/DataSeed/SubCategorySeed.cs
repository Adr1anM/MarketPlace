using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;


namespace MarketPlace.Infrastructure.DataSeed
{
    public class SubCategorySeed
    {
        public static async Task Seed(ArtMarketPlaceDbContext context)
        {
            if (!context.SubCategories.Any())
            {
                var subCategories = new List<SubCategory>()
                {
                    new SubCategory
                    {
                        Name = "Abstract"
                    },
                    new SubCategory
                    {
                        Name = "Contemporary"
                    },
                    new SubCategory
                    {
                        Name = "Landscape"
                    },
                    new SubCategory
                    {
                        Name = "Religious"
                    },
                    new SubCategory
                    {
                        Name = "Figurative"
                    }
                };

                context.SubCategories.AddRange(subCategories);
                await context.SaveChangesAsync();   
            }
        }
    }
}
