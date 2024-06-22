using MarketPlace.Domain.Models.Auth;
using MarketPlace.Infrastructure.DataSeed;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Identity;

namespace MarketPlace.WebUI.Extentions
{
    public static class HostExtentions
    {
        public static async Task SeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try 
                {
                    var context = services.GetRequiredService<ArtMarketPlaceDbContext>();
                    var roleManager = services.GetRequiredService<RoleManager<Role>>();

                    await SeedFacade.SeedData(context, roleManager);

                }
                catch (Exception ex) 
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured during migration");
                }
            }
        }
    }
}
