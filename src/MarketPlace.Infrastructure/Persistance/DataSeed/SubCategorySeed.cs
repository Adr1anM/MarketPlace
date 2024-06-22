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
                    },
                    new SubCategory
                    {
                        Name = "Portrait"
                    },
                    new SubCategory
                    {
                        Name = "Street"
                    },
                    new SubCategory
                    {
                        Name = "Ceramics"
                    },
                    new SubCategory
                    {
                        Name = "Textile"
                    },
                    new SubCategory
                    {
                        Name = "Woodworking"
                    },
                    new SubCategory
                    {
                        Name = "Pencil"
                    },
                    new SubCategory
                    {
                        Name = "Oil"
                    }
                };

                context.SubCategories.AddRange(subCategories);
                await context.SaveChangesAsync();   
            }
        }
    }
}
