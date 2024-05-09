using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Update;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Domain.Models;
using MarketPlace.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUi.Tests.Helpers;

namespace WebUi.Tests.ControllersTests.IntegrationTests.Products
{
    public class ProductsControllerTests : ControllerTestFixture
    {
        private readonly ControllerTestFixture _fixture;
        private readonly ProductController _controller;

        public ProductsControllerTests(ControllerTestFixture fixture, ProductController controller)
        {
            _fixture = fixture;
            _controller = controller;
        }

        [Fact]
        public async Task CreateOrder_ReturnsOkResult_And_OrderDto()
        {

            var context = _fixture.GetContext();
            var command = new CreateOrder(1, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);

            // Act
            var result = await _controller.CreateOrder(command);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var orderDto = Assert.IsType<OrderDto>(createdResult.Value);
            Assert.Equal(command.CreatedDate, orderDto.CreatedDate);

            SeedHelper.CleanDatabase(context);
        }


        [Fact]
        public async Task GetOrderByid_ReturnsOkResult_And_OrderDto()
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


        [Fact]
        public async Task Update_Order_Returns_OkObjectResult_And_OrderDto()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedOrders(context);
            var user = new User { Id = 1, UserName = "TestUser" };
            var promocode = new Promocode { Id = 1, Code = "DISCOUNT10" };

            var command = new UpdateOrder(1, user.Id, DateTime.Now, promocode.Id, 2, "123 Main St, Anytown USA", OrderStatus.Processing.Id, 49.99m);

            var orderDto = new OrderDto
            {
                Id = command.Id,
                CretedById = command.CretedById,
                CreatedDate = command.CreatedDate,
                PromocodeId = command.PromocodeId,
                Quantity = command.Quantity,
                ShippingAdress = command.ShippingAdress,
                StatusId = command.StatusId,
                TotalPrice = command.TotalPrice,
            };

            //Act
            var result = await _controller.UpdateOrder(command);

            var response = Assert.IsType<OkObjectResult>(result);
            var objectResult = Assert.IsType<OrderDto>(response.Value);

            Assert.Multiple(() =>
            {
                Assert.Equal(orderDto.TotalPrice, objectResult.TotalPrice);
                Assert.Equal(orderDto.Quantity, objectResult.Quantity);
                Assert.Equal(orderDto.ShippingAdress, objectResult.ShippingAdress);
                Assert.Equal(orderDto.StatusId, objectResult.StatusId);
            });
            SeedHelper.CleanDatabase(context);
        }


        [Fact]
        public async Task Update_InvalideOrder_Returns_NotFoundObjectResult()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedOrders(context);
            var user = new User { Id = 1, UserName = "TestUser" };
            var promocode = new Promocode { Id = 1, Code = "DISCOUNT10" };

            var command = new UpdateOrder(20, user.Id, DateTime.Now, promocode.Id, 2, "123 Main St, Anytown USA", OrderStatus.Processing.Id, 49.99m);

            //Act
            var result = await _controller.UpdateOrder(command);

            var response = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal($"Order with Id:{command.Id} not found", response.Value);
            SeedHelper.CleanDatabase(context);
        }

        [Fact]
        public async Task Delete_Order_Returns_OkObjectResult_And_OrderDto()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedOrders(context);
            int orderId = 1;

            var orderDto = new OrderDto
            {
                Id = 1,
                CretedById = 1,
                CreatedDate = DateTime.Now,
                PromocodeId = 1,
                Quantity = 2,
                ShippingAdress = "123 Main St, Anytown USA",
                StatusId = OrderStatus.Processing.Id,
                TotalPrice = 49.99m
            };

            //Act
            var result = await _controller.DeleteOrder(orderId);

            var response = Assert.IsType<OkObjectResult>(result);
            var objectResult = Assert.IsType<OrderDto>(response.Value);

            Assert.Multiple(() =>
            {
                Assert.Equal(orderDto.TotalPrice, objectResult.TotalPrice);
                Assert.Equal(orderDto.Quantity, objectResult.Quantity);
                Assert.Equal(orderDto.ShippingAdress, objectResult.ShippingAdress);
                Assert.Equal(orderDto.StatusId, objectResult.StatusId);
            });

            SeedHelper.CleanDatabase(context);

        }

        [Fact]
        public async Task Delete_InvalideOrder_Returns_NotFoundException()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedOrders(context);
            int orderId = 20;

            //Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _controller.DeleteOrder(orderId));
            Assert.Equal("No such Order found", exception.Message);

            SeedHelper.CleanDatabase(context);
        }

        [Fact]
        public async Task GetAllOrders_Returns_OkObjectResult_And_ListOf_OrdersDto()
        {
            //Arrange
            var context = _fixture.GetContext();
            var expectedOrderList = SeedHelper.GetAllSeededOrders().ToList();
            SeedHelper.SeedOrders(context);

            //Act
            var result = await _controller.GetAllOrders();

            //Assert
            var response = Assert.IsType<OkObjectResult>(result);
            var actualOrderList = Assert.IsAssignableFrom<IEnumerable<OrderDto>>(response.Value);

            var sortedActualOrderList = actualOrderList.OrderBy(x => x.Id).ToList();

            Assert.Equal(expectedOrderList.Count(), actualOrderList.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expectedOrderList.Count; i++)
                {
                    var expectedOrder = expectedOrderList[i];
                    var actualOrder = sortedActualOrderList[i];

                    Assert.Equal(expectedOrder.Id, actualOrder.Id);
                    Assert.Equal(expectedOrder.CretedById, actualOrder.CretedById);
                    Assert.Equal(expectedOrder.PromocodeId, actualOrder.PromocodeId);
                    Assert.Equal(expectedOrder.Quantity, actualOrder.Quantity);
                    Assert.Equal(expectedOrder.ShippingAdress, actualOrder.ShippingAdress);
                    Assert.Equal(expectedOrder.StatusId, actualOrder.StatusId);
                    Assert.Equal(expectedOrder.TotalPrice, actualOrder.TotalPrice);
                }
            });
            SeedHelper.CleanDatabase(context);
        }

        [Fact]
        public async Task GetAllOrders_Returns_NotFoundObjectResult()
        {
            //Arrange
            var context = _fixture.GetContext();

            //Act
            var result = await _controller.GetAllOrders();

            var response = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("There are no Orders", response.Value);
            SeedHelper.CleanDatabase(context);
        }
    }
}
