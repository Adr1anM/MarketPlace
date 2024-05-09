using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.App.Orders.Responses;
using Application.UnitTests.Helpers;

namespace Application.UnitTests.CommandTests.OrderCommandTests
{
    public class CreateOrderHandlerTest : BaseCommandHandlerTest
    {

        [Fact]
        public async Task Create_OrderCommand_Should_Create_ReturnsOrderDto()
        {
            //Arrange
            var command = new CreateOrder(1, DateTime.Now, 231 ,3, "santafe 32/1", 3 , 1230);

            var order = new Order
            {
                Id = 1,
                CretedById = command.CretedById,
                CreatedDate = command.CreatedDate,
                PromocodeId = command.PromocodeId,  
                Quantity = command.Quantity,
                ShippingAdress = command.ShippingAdress,
                StatusId = command.StatusId,
                TotalPrice = command.TotalPrice,
            };
            var orderDto = new OrderDto
            {
                Id = order.Id,
                CretedById = command.CretedById,
                CreatedDate = command.CreatedDate,
                PromocodeId = command.PromocodeId,
                Quantity = command.Quantity,
                ShippingAdress = command.ShippingAdress,
                StatusId = command.StatusId,
                TotalPrice = command.TotalPrice,
            };

            var handler = new CreateOrderHandler(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerFactoryMock.Object);


            _mapperMock.Setup(m => m.Map<Order>(command)).Returns(order);
            _unitOfWorkMock.Setup(uow => uow.Orders.AddAsync(order)).ReturnsAsync(order);
            _mapperMock.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);
            _unitOfWorkMock.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask);

            //Act
            var result = await handler.Handle(command, default);

            //Assert

            Assert.NotNull(result);
            Assert.IsType<OrderDto>(result);
        }


        [Fact]
        public async Task Create_OrderCommand_Should_Return_Null_When_MappingFail()
        {
            var command = new CreateOrder(1, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);

            var order = new Order
            {
                Id = 1,
                CretedById = command.CretedById,
                CreatedDate = command.CreatedDate,
                PromocodeId = command.PromocodeId,
                Quantity = command.Quantity,
                ShippingAdress = command.ShippingAdress,
                StatusId = command.StatusId,
                TotalPrice = command.TotalPrice,
            };

            var orderDto = new OrderDto
            {
                Id = order.Id,
                CretedById = command.CretedById,
                CreatedDate = command.CreatedDate,
                PromocodeId = command.PromocodeId,
                Quantity = command.Quantity,
                ShippingAdress = command.ShippingAdress,
                StatusId = command.StatusId,
                TotalPrice = command.TotalPrice,
            };

            var handler = new CreateOrderHandler(
                _unitOfWorkMock.Object,
                _mapperMock.Object,
                _loggerFactoryMock.Object);

            _mapperMock.Setup(m => m.Map<Order>(command)).Returns((Order)null);
            _unitOfWorkMock.Setup(uow => uow.Orders.AddAsync(order)).ReturnsAsync(order);
            _mapperMock.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);
            _unitOfWorkMock.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask);

            var result = await handler.Handle(command, default);

            // Act & Assert
            Assert.Null(result);
            
        }
    }
}
