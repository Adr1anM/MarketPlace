using AutoMapper;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Update;
using MarketPlace.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.CommandTests.OrderCommandTests
{
    public class UpdateOrderHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILoggerFactory> _loggerMock;

        public UpdateOrderHandlerTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILoggerFactory>();
        }

        [Fact]
        public async Task UpdateOrder_Command_Should_Update_ReturnsOrderDto()
        {
            var command = new UpdateOrder(1,3, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);

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

            var updatedOrder = new Order
            {
                Id = order.Id,
                CretedById = command.CretedById,
                CreatedDate = command.CreatedDate,
                PromocodeId = command.PromocodeId,
                Quantity = command.Quantity,
                ShippingAdress = command.ShippingAdress,
                StatusId = command.StatusId,
                TotalPrice = 9000,
            };

            var handler = new UpdateOrderHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);

            _unitOfWorkMock.Setup(uaw => uaw.Orders.GetByIdAsync(command.Id)).ReturnsAsync(order);

            _mapperMock.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);
            _mapperMock.Setup(m => m.Map(command, order)).Returns(updatedOrder);


            var result = await handler.Handle(command, default);

            // Assert   

            _unitOfWorkMock.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);

        }

        [Fact]
        public async Task Handle_ExceptionThrownDuringHandling_LogsErrorAndRollsBackTransaction()
        {
            // Arrange
            var command = new UpdateOrder(1, 3, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);
            var author = new Order();

            _unitOfWorkMock.Setup(uow => uow.Orders.GetByIdAsync(command.Id)).ReturnsAsync(author);

            var handler = new UpdateOrderHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
            var expectedExceptionMessage = "Test exception message";
            _mapperMock.Setup(m => m.Map(command, author)).Throws(new Exception(expectedExceptionMessage));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(command, default));
            _unitOfWorkMock.Verify(uow => uow.RollbackTransactionAsync(), Times.Once);
        }
    }
}
