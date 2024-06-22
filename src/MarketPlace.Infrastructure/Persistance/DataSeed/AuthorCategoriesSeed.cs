using MarketPlace.Domain.Models;
using MarketPlace.Infrastructure.Persistance.Context;

namespace MarketPlace.Infrastructure.DataSeed
{
    public class AuthorCategoriesSeed
    {
        public static async Task Seed(ArtMarketPlaceDbContext context)
        {
            if (!context.AuthorCategories.Any())
            {
                var authorCategories = new List<AuthorCategory>()
                {
                    new AuthorCategory()
                    {
                        Name = "Painter"
                    },

                    new AuthorCategory()
                    {
                        Name = "Photographer"
                    },
                    new AuthorCategory()
                    {
                        Name = "Sculptor"
                    }
                };

                context.AuthorCategories.AddRange(authorCategories);
                await context.SaveChangesAsync();
            }
        }
    }
}
