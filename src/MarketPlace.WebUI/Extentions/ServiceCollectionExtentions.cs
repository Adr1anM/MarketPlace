 using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MarketPlace.Infrastructure.Persistance.Extentions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Infrastructure.Repositories;
using MarketPlace.Application.Abstractions;
using MarketPlace.Infrastructure;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Extensions;
using MarketPlace.Application.Profiles;
using MarketPlace.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using MarketPlace.Infrastructure.Persistance.Context;
using FluentValidation;
using MarketPlace.Application.Abstractions.Behaviors.Messaging;
using MediatR;
using MarketPlace.Application.Abstractions.Behaviors;
using MarketPlace.Application.App.Products.CommandValidator;

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
            builder.Services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<ArtMarketPlaceDbContext>()
            .AddDefaultTokenProviders();
            builder.Services.AddScoped<RoleManager<Role>>();


        }
    }

}
