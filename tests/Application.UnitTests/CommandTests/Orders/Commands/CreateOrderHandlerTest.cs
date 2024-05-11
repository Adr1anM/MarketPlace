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
using Microsoft.AspNetCore.Identity;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Paints.Responses;
using Azure.Core;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.Exceptions;

namespace Application.UnitTests.CommandTests.Orders.Commands
{
    public class CreateOrderHandlerTest : BaseCommandHandlerTest
    {

        private readonly Mock<IGenericRepository<Promocode>> _promocodeGenericRepo;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly CreateOrderHandler _handler;
        public CreateOrderHandlerTest()
        {
            _promocodeGenericRepo = new Mock<IGenericRepository<Promocode>>();
            _userManagerMock = MockUserManager<User>();
            _handler = new CreateOrderHandler(
            _userManagerMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _loggerFactoryMock.Object);
        }

        private Order CreateTestOrder(CreateOrder command)
        {
            return new Order
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
        }
        private OrderDto CreateTestOrderDto(CreateOrder command)
        {
            return new OrderDto
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
        }
        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }
        [Fact]
        public async Task Create_OrderCommand_Should_Create_ReturnsOrderDto()
        {
            //Arrange
            var command = new CreateOrder(1, DateTime.Now, 2, 3, "santafe 32/1", 3, 1230);

            var order = CreateTestOrder(command);
            var orderDto = CreateTestOrderDto(command);
            var user = new User { Id = command.CretedById };
            var promocode = new Promocode { Id = 2, Code = "qqqwwww", CreatedDate = DateTime.UtcNow, ExpireDate = DateTime.UtcNow };

            _userManagerMock.Setup(u => u.FindByIdAsync(It.Is<string>(x => x == command.CretedById.ToString()))).ReturnsAsync(user);
            _unitOfWorkMock.Setup(uow => uow.GetGenericRepository<Promocode>()).Returns(_promocodeGenericRepo.Object);
            _promocodeGenericRepo.Setup(repo => repo.GetByIdAsync(command.PromocodeId)).ReturnsAsync(promocode);

            _mapperMock.Setup(m => m.Map<Order>(command)).Returns(order);
            _unitOfWorkMock.Setup(uow => uow.Orders.AddAsync(order)).ReturnsAsync(order);
            _mapperMock.Setup(m => m.Map<OrderDto>(order)).Returns(orderDto);
            _unitOfWorkMock.Setup(uow => uow.SaveAsync(CancellationToken.None)).Returns(Task.CompletedTask);

            //Act
            var result = await _handler.Handle(command, default);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<OrderDto>(result);
            Assert.Multiple(() =>
            {
                Assert.Equal(command.TotalPrice, result.TotalPrice);
                Assert.Equal(command.StatusId, result.StatusId);
                Assert.Equal(command.Quantity, result.Quantity);
                Assert.Equal(command.TotalPrice, result.TotalPrice);
                Assert.Equal(command.PromocodeId, result.PromocodeId);
            });

        }


        [Fact]
        public async Task InvalidUserId_Returns_EntityNotFoundException()
        {
            //Arrange
            var command = new CreateOrder(1, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);
            var notFoundException = new EntityNotFoundException(typeof(User), command.CretedById);
            _userManagerMock.Setup(u => u.FindByIdAsync(It.Is<string>(x => x == command.CretedById.ToString()))).Throws(notFoundException);

            // Act & Assert
            var exceptionResult = await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Entity of type '{typeof(User).Name}' with ID '{command.CretedById}' not found.", exceptionResult.Message);
        }

        [Fact]
        public async Task InvalidPromocodeId_Returns_EntityNotFoundException()
        {
            //Arrange
            var command = new CreateOrder(1, DateTime.Now, 231, 3, "santafe 32/1", 3, 1230);
            var notFoundException = new EntityNotFoundException(typeof(Promocode), command.PromocodeId);
            var user = new User { Id = command.CretedById };
       
            _userManagerMock.Setup(u => u.FindByIdAsync(It.Is<string>(x => x == command.CretedById.ToString()))).ReturnsAsync(user);
            _unitOfWorkMock.Setup(uow => uow.GetGenericRepository<Promocode>()).Returns(_promocodeGenericRepo.Object);
            _promocodeGenericRepo.Setup(p => p.GetByIdAsync(command.PromocodeId)).Throws(notFoundException);
 
            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
