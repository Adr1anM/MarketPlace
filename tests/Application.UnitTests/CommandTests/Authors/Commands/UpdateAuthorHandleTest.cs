using Application.UnitTests.Helpers;
using AutoMapper;
using Azure.Core;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Exceptions;
using MarketPlace.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace Application.UnitTests.CommandTests.Authors.Comands
{
    public class UpdateAuthorHandleTest : BaseCommandHandlerTest
    {
        private readonly UpdateAuthorHandler _handler;
        public UpdateAuthorHandleTest()
        {
            _handler = new UpdateAuthorHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerFactoryMock.Object);
        }
        private Author CreateTestAuthor(UpdateAuthor command)
        {
            return new Author
            {
                Id = command.Id,
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts,
            };
        }

        private AuthorDto CreateTestAuthorDto(UpdateAuthor command)
        {
            return new AuthorDto
            {
                Id = command.Id,
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts,
            };
        }

        [Fact]
        public async Task UpdateAuthor_Command_Should_Update_ReturnsAuthorDto()
        {
            var command = new UpdateAuthor(2, 3, "biography", "moldova", DateTime.Now, "SocialMedia", 10);

            var author = new Author
            {
                Id = command.Id,
                UserId = command.UserId,
                Biography = command.Biography,
                Country = "france",
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = 5,
            };

            var updatedAuthor = CreateTestAuthor(command);

            var authorDto = CreateTestAuthorDto(command);

            var handler = new UpdateAuthorHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerFactoryMock.Object);

            _unitOfWorkMock.Setup(uaw => uaw.Authors.GetByIdAsync(command.Id)).ReturnsAsync(author);

            _mapperMock.Setup(m => m.Map<AuthorDto>(author)).Returns(authorDto);
            _mapperMock.Setup(m => m.Map(command, author)).Returns(updatedAuthor);


            var result = await handler.Handle(command, default);

            // Assert   
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
            Assert.Equal(command.Country, result.Country);
            Assert.Equal(command.NumberOfPosts, result.NumberOfPosts);

        }

        [Fact]
        public async Task Handle_ExceptionThrownDuringHandling_LogsError()
        {
            // Arrange
            var command = new UpdateAuthor(2, 3, "biography", "moldova", DateTime.Now, "SocialMedia", 10);
            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(command.Id)).ReturnsAsync((Author)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Entity of type '{typeof(Author).Name}' with ID '{command.Id}' not found.", exception.Message);
        }

        [Fact]
        public async Task Update_Author_Throw_AutoMapperMappingException()
        {
            //Arrange
            var command = new UpdateAuthor(2, 3, "biography", "moldova", DateTime.UtcNow, "SocialMedia", 10);
            var author = new Author();
            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(command.Id)).ReturnsAsync(author);
            _mapperMock.Setup(m => m.Map(It.IsAny<UpdateAuthor>(), It.IsAny<Author>())).Throws<AutoMapperMappingException>();

            // Act & Assert
            await Assert.ThrowsAsync<AutoMapperMappingException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
