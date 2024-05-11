using Application.UnitTests.Helpers;
using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using Microsoft.AspNetCore.Identity;
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
    public class DeleteOrderHandlerTest : BaseCommandHandlerTest
    {
        private readonly DeleteOrderHandler _handler;
        public DeleteOrderHandlerTest()
        {
 
            _handler = new DeleteOrderHandler(
            _mapperMock.Object,
            _unitOfWorkMock.Object,
            _loggerFactoryMock.Object);
        }

        private Order CreateTestOrder(int orderId)
        {
            return new Order
            {
                Id = orderId,
                CretedById = 3,
                CreatedDate = DateTime.Now,
                PromocodeId = 23,
                Quantity = 12,
                ShippingAdress = "LOS angeles 3/1",
                StatusId = 4,
                TotalPrice = 9000,
            };
        }
        private OrderDto CreateTestOrderDto(int orderId)
        {
            return new OrderDto
            {
                Id = orderId,
                CretedById = 3,
                CreatedDate = DateTime.Now,
                PromocodeId = 23,
                Quantity = 12,
                ShippingAdress = "LOS angeles 3/1",
                StatusId = 4,
                TotalPrice = 9000,
            };
        }

        [Fact]
        public async Task Handle_ValidOrderId_DeletesOrderAndReturnsOrderDto()
        {
            // Arrange
            int orderId = 1;

            var order = CreateTestOrder(orderId);   
            var orderDto = CreateTestOrderDto(orderId);
            var request = new DeleteOrder(orderId);

            _unitOfWorkMock.Setup(uow => uow.Orders.GetByIdAsync(orderId)).ReturnsAsync(order);
            _unitOfWorkMock.Setup(uow => uow.Orders.DeleteAsync(order.Id)).ReturnsAsync(order);


            _mapperMock.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert   
            _unitOfWorkMock.Verify(uow => uow.Orders.DeleteAsync(order.Id), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public async Task Handle_InvalidOrderId_ThrowsException()
        {
            // Arrange
            int orderId = 1;
            _unitOfWorkMock.Setup(uow => uow.Orders.GetByIdAsync(orderId)).ReturnsAsync((Order)null);
            var request = new DeleteOrder(orderId);


            // Act & Assert
            var exceptionResult = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));
            Assert.Equal($"Entity of type '{typeof(Order).Name}' with ID '{request.id}' not found.",exceptionResult.Message);   

            _unitOfWorkMock.Verify(uow => uow.Orders.DeleteAsync(It.IsAny<int>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Never);

        }
    }
}
