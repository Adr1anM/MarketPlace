using Application.UnitTests.Helpers;
using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UnitTests.CommandTests.OrderCommandTests
{
    public class DeleteOrderHandlerTest : BaseCommandHandlerTest
    {
      
        [Fact]
        public async Task Handle_ValidOrderId_DeletesOrderAndReturnsOrderDto()
        {
            // Arrange
            int orderId = 1;

            var order = new Order
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
            var orderDto = new OrderDto
            {
                Id = order.Id,
                CretedById = 3,
                CreatedDate = DateTime.Now,
                PromocodeId = 23,
                Quantity = 12,
                ShippingAdress = "LOS angeles 3/1",
                StatusId = 4,
                TotalPrice = 9000,
            };

            var handler = new DeleteOrderHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerFactoryMock.Object);
            var request = new DeleteOrder(orderId);

            _unitOfWorkMock.Setup(uow => uow.Orders.GetByIdAsync(orderId)).ReturnsAsync(order);
            _unitOfWorkMock.Setup(uow => uow.Orders.DeleteAsync(order.Id)).ReturnsAsync(order);


            _mapperMock.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);

            // Act
            var result = await handler.Handle(request, default);

            // Assert   

            _unitOfWorkMock.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Orders.DeleteAsync(order.Id), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(orderId, result.Id);
        }

        [Fact]
        public async Task Handle_InvalidOrderId_ThrowsException()
        {
            // Arrange
            int orderId = 1;

            _unitOfWorkMock.Setup(uow => uow.Orders.GetByIdAsync(orderId)).ReturnsAsync((Order)null);

            var handler = new DeleteAuthorHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerFactoryMock.Object);
            var request = new DeleteAuthor(orderId);


            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(request, CancellationToken.None));

            _unitOfWorkMock.Verify(uow => uow.BeginTransactionAsync(), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Orders.DeleteAsync(It.IsAny<int>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Never);


        }
    }
}
