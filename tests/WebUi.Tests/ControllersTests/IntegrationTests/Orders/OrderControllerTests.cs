using AutoMapper;
using Azure.Core;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Application.Orders.Update;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Application.Products.Delete;
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
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebUi.Tests.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebUi.Tests.ControllersTests.IntegrationTests.Orders
{
    public class OrderControllerTests : IDisposable
    {
        private readonly BaseWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public OrderControllerTests()
        {
            _factory = new BaseWebApplicationFactory();
            _client = _factory.CreateClient();
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            SeedHelper.SeedDatabase(context);
        }

        [Fact]
        public async Task CreateOrder_ReturnsOkResult_And_OrderDto()
        {

            //Arrange
            var command = new CreateOrder(1, DateTime.Now, 2, 3, "santafe 32/1", 3, 1230);

            // Act
            var result = await _client.PostAsJsonAsync("api/Orders", command);
            var createdOrder = await result.Content.ReadFromJsonAsync<OrderDto>();

            //Assert
            Assert.NotNull(createdOrder);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                Assert.Equal(command.CretedById, createdOrder.CretedById);
                Assert.Equal(command.TotalPrice, createdOrder.TotalPrice);
                Assert.Equal(command.StatusId, createdOrder.StatusId);
                Assert.Equal(command.Quantity, createdOrder.Quantity);
            });

        }

        [Fact]
        public async Task Create_Order_WithNonExisting_UserId_ReturnsNotFound()
        {
            //Assert
            var command = new CreateOrder(20, DateTime.Now, 2, 3, "santafe 32/1", 3, 1230);

            //Act
            var response = await _client.PostAsJsonAsync("api/Orders", command);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }

        [Fact]
        public async Task Create_Order_WithNonExisting_PromocodeId_ReturnsNotFound()
        {
            //Assert
            var command = new CreateOrder(1, DateTime.Now, 10, 3, "santafe 32/1", 3, 1230);

            //Act
            var response = await _client.PostAsJsonAsync("api/Orders", command);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }

        [Fact]
        public async Task Create_Order_WithInvalidParams_ReturnsBadRequest()
        {
            var command = new CreateOrder(1, DateTime.Now, 0, 0, "santafe 32/1", 3, 1230); 

            var response = await _client.PostAsJsonAsync("api/Orders", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }

        [Fact]
        public async Task Update_Order_ReturnsOkResult_And_OrderDto()
        {
            //Arrange
            var command = new UpdateOrder(1,1, DateTime.Now, 1, 2, "santafe 32/1", 3, 1230);
            //Act
            var response = await _client.PutAsJsonAsync("api/Orders", command);
            var updatedProduct = await response.Content.ReadFromJsonAsync<OrderDto>();

            //Assert
            Assert.NotNull(updatedProduct);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(command.CretedById, updatedProduct.CretedById);
                Assert.Equal(command.TotalPrice, updatedProduct.TotalPrice);
                Assert.Equal(command.StatusId, updatedProduct.StatusId);
                Assert.Equal(command.Quantity, updatedProduct.Quantity);
            });
        }


        [Fact]
        public async Task UpdateOrder_InvalidId_Returns_NotFound()
        {
            //Arrange
            int orderId = 20;
            var command = new UpdateOrder(orderId, 1, DateTime.Now, 1, 2, "santafe 32/1", 3, 1230); 

            //Act
            var response = await _client.PutAsJsonAsync("api/Orders", command);
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.NotEmpty(content);
            });
        }

        [Fact]
        public async Task Update_Order_WithInvalidParams_ReturnsBadRequest()
        {
            var command = new UpdateOrder(1, 1, DateTime.Now, 0, 0, "santafe 32/1", 3, 1230);

            var response = await _client.PutAsJsonAsync("api/Orders", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }

        [Fact]
        public async Task DeleteOrder_Returns_OkResult_AndOrderDto()
        {
            //Arrange
            int orderId = 1;
            var command = new DeleteOrder(orderId);

            //Act
            var response = await _client.DeleteAsync($"api/Orders/{orderId}");
            var result = await response.Content.ReadFromJsonAsync<OrderDto>();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var deletedOrder = context.Orders.Find(orderId);

            // Assert
            Assert.NotNull(result);
            Assert.Multiple(() =>
            {

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(orderId, result.Id);
                Assert.Null(deletedOrder);
            });

        }

        [Fact]
        public async Task DeleteOrder_InvalidId_Returns_NotFound()
        {
            //Arrange
            int orderId = 12;
            var command = new DeleteOrder(orderId);


            //Act
            var response = await _client.DeleteAsync($"api/Orders/{orderId}");
            var result = await response.Content.ReadAsStringAsync();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var deletedProduct = context.Orders.Find(orderId);

            // Assert
            Assert.NotEmpty(result);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.Null(deletedProduct);
            });

        }

        [Fact]
        public async Task GetAllOrders_ReturnsAllOrdersDto_OkResult()
        {
            //Act
            var response = await _client.GetAsync("api/Orders/all");
            var orders = await response.Content.ReadFromJsonAsync<IEnumerable<OrderDto>>();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var existingOrders = context.Orders.ToList();

            //Assert
            Assert.NotNull(orders);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                foreach (var existingOrder in existingOrders)
                {
                    var orderDto = orders.FirstOrDefault(p => p.Id == existingOrder.Id);
                    Assert.NotNull(orderDto);
                    Assert.Equal(existingOrder.CretedById, orderDto.CretedById);
                    Assert.Equal(existingOrder.TotalPrice, orderDto.TotalPrice);
                    Assert.Equal(existingOrder.StatusId, orderDto.StatusId);
                    Assert.Equal(existingOrder.Quantity, orderDto.Quantity);
                }                
            });
        }

        [Fact]
        public async Task GetOrderById_Returns_OrderDto()
        {
            //Arrange
            int orderId = 1;

            //Act
            var response = await _client.GetAsync($"api/Orders/{orderId}");
            var order = await response.Content.ReadFromJsonAsync<Order>();

            //Assert
            Assert.NotNull(order);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(orderId, order.Id);
            });
        }



        [Fact]
        public async Task GetOrderBy_InvalidId_Returns_NotFoundResponse()
        {
            //Arrange
            int orderId = 13;

            //Act
            var response = await _client.GetAsync($"api/Orders/{orderId}");

            //Assert       
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);          
        }

        public void Dispose()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            context.Database.EnsureDeleted();

            _factory.Dispose();
            _client.Dispose();
        }
       
    }
}
