using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Infrastructure;
using MarketPlace.Infrastructure.Persistance.Context;
using MarketPlace.Infrastructure.Repositories;
using MarketPlace.WebUI.Controllers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebUi.Tests.Helpers;

namespace WebUi.Tests.ControllersTests.IntegrationTests
{
    public class OrderControllerTests : IClassFixture<OrdersControllerTestFixture>
    {
        private readonly OrdersControllerTestFixture _fixture;
        private readonly OrdersController _controller;

        public OrderControllerTests(OrdersControllerTestFixture factory)
        {
            _fixture = factory;
            _controller = new OrdersController(_fixture.Mediator);
        }

        [Fact]
        public async Task CreateOrder_ReturnsOkResult()
        {

            var context = _fixture.GetContext();
            var command = new CreateOrder(1, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);

            // Act
            var result = await _controller.CreateOrder(command);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var authorDto = Assert.IsType<OrderDto>(createdResult.Value);
            Assert.Equal(command.CreatedDate, authorDto.CreatedDate);

            SeedHelper.CleanDatabase(context);
        }   




        [Fact]
        public async Task GetOrderByid_ReturnsOkResult()
        {

            // Arrange
            var context = _fixture.GetContext();

            SeedHelper.SeedOrders(context);

            var user = new User { Id = 1, UserName = "TestUser" };
            var promocode = new Promocode { Id = 1, Code = "DISCOUNT10" };

            var order = new Order
            {
                Id = 1,
                CretedById = user.Id,
                CreatedDate = DateTime.Now,
                PromocodeId = promocode.Id,
                Promocode = promocode,
                Quantity = 2,
                ShippingAdress = "123 Main St, Anytown USA",
                StatusId = OrderStatus.Processing.Id,
                TotalPrice = 49.99m
            };


            // Act

            var result = await _controller.GetOrderById(order.Id);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var authorDto = Assert.IsType<OrderDto>(createdResult.Value);

            Assert.Multiple(() =>
            {
                Assert.Equal(order.CretedById, authorDto.CretedById);
                Assert.Equal(order.Quantity, authorDto.Quantity);
                Assert.Equal(order.ShippingAdress, authorDto.ShippingAdress);
                Assert.Equal(order.StatusId, authorDto.StatusId);

            });

            SeedHelper.CleanDatabase(context);

        }


        [Fact]
        public async Task If_Order_Not_Present_GetOrderByid_ReturnsNotFoundResult()
        {

            // Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedOrders(context);

            var user = new User { Id = 4, UserName = "TestUser" };
            var promocode = new Promocode { Id = 3, Code = "DISCOUNT10" };

            var order = new Order
            {
                Id = 6,
                CretedById = user.Id,
                CreatedDate = DateTime.Now,
                PromocodeId = promocode.Id,
                Quantity = 5,
                ShippingAdress = "789 Maple Ave, Someplace USA",
                StatusId = OrderStatus.Processing.Id,
                TotalPrice = 49.99m
            };

            // Act

            var result = await _controller.GetOrderById(order.Id);

            // Assert
            Assert.IsType<NotFoundObjectResult>(result);

            SeedHelper.CleanDatabase(context);
        }



    }
    
}
