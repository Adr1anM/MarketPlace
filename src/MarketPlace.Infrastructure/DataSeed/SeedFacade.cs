

using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Infrastructure.DataSeed
{
    public class SeedFacade
    {
        public static async Task SeedData(ArtMarketPlaceDbContext context)
        {
            context.Database.Migrate();
            await AuthorCategoriesSeed.Seed(context);
            await CategorySeed.Seed(context);
            await SubCategorySeed.Seed(context);
            await AuthorsSeed.Seed(context);
            await ProductsSeed.Seed(context);
        }
    }
}
