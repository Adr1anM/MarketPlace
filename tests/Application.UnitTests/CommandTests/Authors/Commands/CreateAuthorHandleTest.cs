/*using System;
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
using Application.UnitTests.Helpers;
using MarketPlace.Application.Exceptions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.UnitTests.CommandTests.Authors.Comands
{
    public class CreateAuthorHandleTest : BaseCommandHandlerTest
    {
        private readonly Mock<UserManager<User>> _userManagerMock;
        private readonly CreateAuthorHandler _handler;
        public CreateAuthorHandleTest()
        {
            _userManagerMock = MockUserManager<User>();
            _handler = new CreateAuthorHandler(
            _userManagerMock.Object,
            _mapperMock.Object,
            _unitOfWorkMock.Object,
            _loggerFactoryMock.Object);
        }

        private Author CreateTestAuthor(CreateAuthor command)
        {
            return new Author
            {
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts
            };
        }

        private AuthorDto CreateTestAuthorDto(CreateAuthor command)
        {
            return new AuthorDto
            {
                Id = command.UserId,
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts
            };
        }
        private static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }


        [Fact]
    public async Task Create_AuthorComand_Should_Create_ReturnsAuthorDto()
    {
        // Arrange
        var command = new CreateAuthor(1, "Biography", "Country", DateTime.Now, "SocialMedia", 10);
        var author = CreateTestAuthor(command);
        var authorDto = CreateTestAuthorDto(command);

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
        _unitOfWorkMock.Setup(uow => uow.SaveAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        // Act
        var result = await handler.Handle(command, default);
        _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);
            // Assert
            Assert.NotNull(result);
        Assert.Equal(result.UserId, command.UserId);
        Assert.IsType<AuthorDto>(result);
    }



        [Fact]
        public async Task Create_AuthorComand_Should_Throw_Exception_When_UserNotFound()
        {
            // Arrange
            var command = new CreateAuthor(1, "Biography", "Country", DateTime.Now, "SocialMedia", 10);
            var notFoundException = new EntityNotFoundException(typeof(Author), command.UserId);

            _userManagerMock.Setup(u => u.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((User)null);

            // Act & Assert
            var exceptionResult = await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command, default));
            Assert.Equal($"Entity of type '{typeof(Author).Name}' with ID '{command.UserId}' not found.", exceptionResult.Message);
        }
    }



}
*/