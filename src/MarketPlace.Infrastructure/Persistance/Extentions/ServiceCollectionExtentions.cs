using MarketPlace.Domain.Models.Auth;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace MarketPlace.Infrastructure.Persistance.Extentions
{
    public static class ServiceCollectionExtentions 
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services , IConfiguration dbConfiguration) 
        {
            services.AddDbContext<ArtMarketPlaceDbContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(dbConfiguration.GetConnectionString("ArtMarketConnection"));

            });

            services.AddIdenttyJwtAuthentication();


            return services;
        }
    }
}
