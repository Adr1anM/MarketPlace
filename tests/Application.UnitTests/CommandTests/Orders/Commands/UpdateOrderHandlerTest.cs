using Application.UnitTests.Helpers;
using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Application.Orders.Update;
using MarketPlace.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UnitTests.CommandTests.Orders.Commands
{
    public class UpdateOrderHandlerTest : BaseCommandHandlerTest
    {
        private readonly UpdateOrderHandler _handler;
        public UpdateOrderHandlerTest()
        {

            _handler = new UpdateOrderHandler(
            _mapperMock.Object,
            _unitOfWorkMock.Object,
            _loggerFactoryMock.Object);
        }

        private Order CreateTestOrder(UpdateOrder command)
        {
            return new Order
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
        }
        private OrderDto CreateTestOrderDto(UpdateOrder command)
        {
            return new OrderDto
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
        }


        [Fact]
        public async Task UpdateOrder_Command_Should_Update_ReturnsOrderDto()
        {
            var command = new UpdateOrder(1, 3, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);

            var order = new Order
            {
                Id = 1,
                CretedById = 3,
                CreatedDate = DateTime.Now,
                PromocodeId = 23,
                Quantity = 12,
                ShippingAdress = "LOS angeles 3/1",
                StatusId = 4,
                TotalPrice = 9000,
            };
            var orderDto = CreateTestOrderDto(command);
            var updatedOrder = CreateTestOrder(command);


            _unitOfWorkMock.Setup(uaw => uaw.Orders.GetByIdAsync(command.Id)).ReturnsAsync(order);
            _mapperMock.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);
            _mapperMock.Setup(m => m.Map(command, order)).Returns(updatedOrder);


            var result = await _handler.Handle(command, default);

            // Assert   
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);

        }

        [Fact]
        public async Task Update_NonExisting_Order_Throw_EntityNotFoundException()
        {
            // Arrange
            var command = new UpdateOrder(1, 3, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);
            var author = new Order();

            _unitOfWorkMock.Setup(uow => uow.Orders.GetByIdAsync(command.Id)).ReturnsAsync((Order)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Entity of type '{typeof(Order).Name}' with ID '{command.Id}' not found.", exception.Message);
        }

        [Fact]
        public async Task Update_Order_Throw_AutoMapperMappingException()
        {
            //Arrange
            var command = new UpdateOrder(1, 3, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);
            var author = new Order();
            _unitOfWorkMock.Setup(uow => uow.Orders.GetByIdAsync(command.Id)).ReturnsAsync(author);
            _mapperMock.Setup(m => m.Map(It.IsAny<UpdateOrder>(), It.IsAny<Order>())).Throws<AutoMapperMappingException>();

            // Act & Assert
            await Assert.ThrowsAsync<AutoMapperMappingException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
