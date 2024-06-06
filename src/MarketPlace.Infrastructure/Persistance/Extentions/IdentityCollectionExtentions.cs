using MarketPlace.Application.Abstractions.Services;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Infrastructure.Identity;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace MarketPlace.Infrastructure.Persistance.Extentions
{
    public static class IdentityCollectionExtentions
    {
        public static IServiceCollection AddIdenttyJwtAuthentication(this IServiceCollection services)
        {
            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.RequireUniqueEmail = true;
            })
                .AddRoles<Role>()
                .AddSignInManager()
                .AddEntityFrameworkStores<ArtMarketPlaceDbContext>();
                
           
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            return services;
        }
    }
}
