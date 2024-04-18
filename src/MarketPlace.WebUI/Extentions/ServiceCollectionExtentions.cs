using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MarketPlace.Infrastructure.Persistance.Extentions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Infrastructure.Repositories;

namespace MarketPlace.WebUI.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        }
    }

}
