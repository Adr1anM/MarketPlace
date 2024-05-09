using Castle.Core.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Querries;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Profiles;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Infrastructure.Persistance.Context;
using MarketPlace.Infrastructure.Repositories;
using MarketPlace.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Moq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Update;
using MarketPlace.Application.Orders.Delete;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Application.App.Orders.Querries;

namespace WebUi.Tests.Helpers
{
    public static class TestHelpers
    {
        public static IMediator CreateMediator(IUnitOfWork unitOfWork, IMapper mapper, ILoggerFactory loggerFactory, UserManager<User> usermanager)
        {
            var services = new ServiceCollection();

            services.AddScoped<IUnitOfWork>(_ => unitOfWork);
            services.AddScoped<IMapper>(_ => mapper);
            services.AddScoped<ILoggerFactory>(_ => loggerFactory);
            services.AddScoped<UserManager<User>>(_ => usermanager);



            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAuthor).Assembly));

            //Register handlers for authors
            services.AddScoped<IRequestHandler<GetAllAuthorsQuerry, IEnumerable<AuthorDto>>, GetAllAuthorsQuerryHandler>();
            services.AddScoped<IRequestHandler<GetAuthorByIdQuerry, AuthorDto>, GetAuthorByIdQuerryHandler>();
            services.AddScoped<IRequestHandler<CreateAuthor, AuthorDto>, CreateAuthorHandler>();
            services.AddScoped<IRequestHandler<UpdateAuthor, AuthorDto>, UpdateAuthorHandler>();
            services.AddScoped<IRequestHandler<DeleteAuthor, AuthorDto>, DeleteAuthorHandler>();

            ///Register handlers for orders
            services.AddScoped<IRequestHandler<GetOrderByIdQuerry, OrderDto>, GetOrderByIdHandler>();
            services.AddScoped<IRequestHandler<CreateOrder, OrderDto>, CreateOrderHandler>();
            services.AddScoped<IRequestHandler<UpdateOrder, OrderDto>, UpdateOrderHandler>();
            services.AddScoped<IRequestHandler<DeleteOrder, OrderDto>, DeleteOrderHandler>();
            ///Register handlers for products

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IMediator>();

        }

        public static void AddServices(IServiceCollection services)
        {

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

        }

        public static UserManager<User> SetupUserManager(ArtMarketPlaceDbContext dbContext)
        {
            var userStore = new UserStore<User, Role, ArtMarketPlaceDbContext, int>(dbContext);
            var userManager = new UserManager<User>(userStore, null, null, null, null, null, null, null, null);
            return userManager;
        }

        public static IMapper SetupMapper()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(OrderProfile).Assembly);
            var sereviceProvider = services.BuildServiceProvider();
            return sereviceProvider.GetRequiredService<IMapper>();   
        }

    
        public static void ConfigureTestServices(IServiceCollection services)
        {
            ConfigureLogging(services);
            ConfigureDbContext(services);
            ConfigureIdentity(services);
        }

        private static void ConfigureDbContext(IServiceCollection services)
        {

            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<ArtMarketPlaceDbContext>));

            if (dbContextDescriptor != null)
            {
                services.Remove(dbContextDescriptor);
            }

            services.AddDbContext<ArtMarketPlaceDbContext>(optionBuilder =>
            {
                optionBuilder.UseInMemoryDatabase("InMemoryDbForTesting")
                     .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
        }

        private static void ConfigureLogging(IServiceCollection services)
        {
            // Configure logging for the test environment
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(ILoggerFactory));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
        }


        private static void ConfigureIdentity(IServiceCollection services)
        {
            // Configure Identity for testing environment
            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<ArtMarketPlaceDbContext>()
                .AddDefaultTokenProviders();
               // .AddUserStore<User>();
            services.AddScoped<RoleManager<Role>>();
            services.AddScoped<ArtMarketPlaceDbContext>();
        }

       

    }
}
    
