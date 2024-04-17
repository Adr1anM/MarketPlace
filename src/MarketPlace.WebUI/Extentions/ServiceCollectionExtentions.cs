using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MarketPlace.Infrastructure.Persistance.Extentions;

namespace MarketPlace.WebUI.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructure(builder.Configuration);
        }
    }

}
