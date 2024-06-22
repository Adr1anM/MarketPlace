using MarketPlace.Infrastructure.Persistance.Extentions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Infrastructure.Repositories;
using MarketPlace.Application.Abstractions;
using MarketPlace.Infrastructure;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Profiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MarketPlace.Infrastructure.Options;
using System.Text;
using MarketPlace.Infrastructure.Persistance.Repositories;
using MarketPlace.Domain.Models;

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
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            builder.Services.AddAutoMapper(typeof(ProductProfile).Assembly);
            builder.Services.AddAuthentication(builder.Configuration);
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
          //  builder.Services.AddHttpContextAccessor();
        }

        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptions = services.ConfigureAuthOptions(configuration);
            services.AddJwtAuthentication(authOptions);
            return services;
        }
        private static JwtOptions ConfigureAuthOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var authOptionsConfigurationSection = configuration.GetSection("JwtOptions");
            services.Configure<JwtOptions>(authOptionsConfigurationSection);
            var authOptions = authOptionsConfigurationSection.Get<JwtOptions>();
            return authOptions;
        }

        private static void AddJwtAuthentication(this IServiceCollection services, JwtOptions authOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SecretKey))
                };
            });
        }

    }

}
