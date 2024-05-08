using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using MarketPlace.Application.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Application.App.Authors.Commands;
using Microsoft.Extensions.Logging;
using Azure.Core;
using MarketPlace.Domain.Models;
using MarketPlace.Application.App.Authors.Responses;
using Microsoft.Extensions.DependencyInjection;

namespace Application.UnitTests.CommandTests.AuthorComandTests
{
    public class CreateAuthorHandleTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly Mock<ILoggerFactory> _loggerFactoryMock;
        

        public CreateAuthorHandleTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _userManagerMock = MockUserManager<User>();
            _loggerFactoryMock = new Mock<ILoggerFactory>();
        }

        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }


        [Fact]
        public async Task Create_AuthorComand_Should_Create_ReturnsAuthorDto()
        {
            //Arrange
            var command = new CreateAuthor(1, "Biography", "Country", DateTime.Now, "SocialMedia", 10);
            var author = new Author
            {
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts
            };

            var authorDto = new AuthorDto
            {
                Id = command.UserId,
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts
            };

            var handler = new CreateAuthorHandler(
                _userManagerMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerFactoryMock.Object);

            var user = new User { Id = command.UserId };

            _userManagerMock.Setup(u => u.FindByIdAsync(It.Is<string>(x => x == command.UserId.ToString()))).ReturnsAsync(user);
            _mapperMock.Setup(m => m.Map<Author>(command)).Returns(author);
            _unitOfWorkMock.Setup(uow => uow.Authors.AddAsync(author)).ReturnsAsync(author);
            _mapperMock.Setup(m => m.Map<AuthorDto>(author)).Returns(authorDto);
            _unitOfWorkMock.Setup(uow => uow.SaveAsync()).Returns(Task.CompletedTask);



            //Act

            var result = await handler.Handle(command, default);

            //Assert

            _unitOfWorkMock.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Authors.AddAsync(author), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(result.UserId, command.UserId);
            Assert.IsType<AuthorDto>(result);
        }


        [Fact]
        public async Task Create_AuthorComand_Should_Throw_Exception_When_UserNotFound()
        {
            // Arrange
            var command = new CreateAuthor(1, "Biography", "Country", DateTime.Now, "SocialMedia", 10);
 
            var handler = new CreateAuthorHandler(
                _userManagerMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerFactoryMock.Object);

            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => handler.Handle(command, default));
        }
    }



}
