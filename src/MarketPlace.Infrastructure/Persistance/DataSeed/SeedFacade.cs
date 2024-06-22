

using MarketPlace.Domain.Models.Auth;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.Infrastructure.DataSeed
{
    public class SeedFacade
    {
        public static async Task SeedData(ArtMarketPlaceDbContext context, RoleManager<Role> roleManager)
        {
            context.Database.Migrate();
            await AuthorCategoriesSeed.Seed(context);
            await CategorySeed.Seed(context);
            await SubCategorySeed.Seed(context);
            await AuthorsSeed.Seed(context);
            await ProductsSeed.Seed(context);
            await UseRolesSeed.Seed(roleManager);
            await CategoriesSubCategSeed.Seed(context); 
        }
    }
}
