using MarketPlace.Infrastructure.Persistance.Extentions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Infrastructure.Repositories;
using MarketPlace.Application.Abstractions;
using MarketPlace.Infrastructure;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MarketPlace.WebUI.OptionsSetup;

namespace MarketPlace.WebUI.Extentions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            builder.Services.ConfigureOptions<JwtOptionsSetup>();
            builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();
                

        }
    }

}
