using AutoMapper;
using Azure.Core;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;


namespace Application.UnitTests.CommandTests.AuthorComandTests
{
    public class UpdateAuthorHandleTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILoggerFactory> _loggerMock;

        public UpdateAuthorHandleTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILoggerFactory>();
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

            var updatedAuthor = new Author
            {
                Id = command.Id,
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts,
            };

            var authorDto = new AuthorDto
            {
                Id = command.Id,
                UserId = command.UserId,
                Biography = command.Biography,
                Country = command.Country,
                BirthDate = command.BirthDate,
                SocialMediaLinks = command.SocialMediaLinks,
                NumberOfPosts = command.NumberOfPosts,
            };

            var handler = new UpdateAuthorHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);

            _unitOfWorkMock.Setup(uaw => uaw.Authors.GetByIdAsync(command.Id)).ReturnsAsync(author);

            _mapperMock.Setup(m => m.Map<AuthorDto>(author)).Returns(authorDto);
            _mapperMock.Setup(m => m.Map(command, author)).Returns(updatedAuthor);


            var result = await handler.Handle(command, default);

            // Assert   

            _unitOfWorkMock.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(command.Id, result.Id);
            Assert.Equal(command.Country, result.Country);
            Assert.Equal(command.NumberOfPosts, result.NumberOfPosts);

        }

        [Fact]
        public async Task Handle_ExceptionThrownDuringHandling_LogsErrorAndRollsBackTransaction()
        {
            // Arrange
            var command = new UpdateAuthor(2, 3, "biography", "moldova", DateTime.Now, "SocialMedia", 10);
            var author = new Author();
            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(command.Id)).ReturnsAsync(author);

            var handler = new UpdateAuthorHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
            var expectedExceptionMessage = "Test exception message"; 
            _mapperMock.Setup(m => m.Map(command, author)).Throws(new Exception(expectedExceptionMessage));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(command, default));
            _unitOfWorkMock.Verify(uow => uow.RollbackTransactionAsync(), Times.Once);
        }
    }
}
