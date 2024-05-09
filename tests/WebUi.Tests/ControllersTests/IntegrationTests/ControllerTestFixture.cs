using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Infrastructure.Persistance.Context;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebUi.Tests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using MarketPlace.WebUI;
using Microsoft.Extensions.Logging;
using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Domain.Models;
using System.Net.NetworkInformation;
using MarketPlace.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;


namespace WebUi.Tests.ControllersTests.IntegrationTests
{
    public class ControllerTestFixture : IDisposable
    {
       
        public IUnitOfWork UnitOfWork { get; }
        public IMediator Mediator { get; }
        public ILoggerFactory LoggerFactory { get; }
        public UserManager<User> UserManager { get; }
        public IMapper Mapper { get; }
        public ArtMarketPlaceDbContext DataContext { get; set; }

        public ServiceProvider serviceProvider { get; }



        public ControllerTestFixture()
        {
            var services = new ServiceCollection();
            TestHelpers.ConfigureTestServices(services);
            TestHelpers.AddServices(services);

            serviceProvider = services.BuildServiceProvider();

            DataContext = serviceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            Mapper = TestHelpers.SetupMapper();
            UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
            LoggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            UserManager = TestHelpers.SetupUserManager(DataContext);
            Mediator = TestHelpers.CreateMediator(UnitOfWork, Mapper, LoggerFactory, UserManager);

        }

        public ArtMarketPlaceDbContext GetContext()
        {
            DataContext = serviceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            return DataContext;
        }

        public void Dispose()
        {
            DataContext?.Dispose();
        }
       
    }
}
