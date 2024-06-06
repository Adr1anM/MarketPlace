using MarketPlace.Infrastructure.DataSeed;
using MarketPlace.Infrastructure.Persistance.Context;

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

                    await SeedFacade.SeedData(context);

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
