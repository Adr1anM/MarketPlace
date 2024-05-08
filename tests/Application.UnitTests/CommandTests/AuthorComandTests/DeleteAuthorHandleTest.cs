using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Application.Orders.Delete;


namespace Application.UnitTests.CommandTests.AuthorComandTests
{
    public class DeleteAuthorHandleTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILoggerFactory> _loggerMock;

        public DeleteAuthorHandleTest()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILoggerFactory>();
        }

        [Fact]
        public async Task Handle_ValidAuthorId_DeletesAuthorAndReturnsAuthorDto()
        {
            // Arrange
            int authorId = 1;

            var author = new Author
            {
                Id = authorId,
                UserId = 2,
                Biography = "Biography",
                Country = "Country",
                BirthDate = DateTime.Now,
                SocialMediaLinks = "SocialMedia",
                NumberOfPosts = 10
            };

            var authorDto = new AuthorDto
            {
                Id = authorId,
                UserId = 2,
                Biography = "Biography",
                Country = "Country",
                BirthDate = DateTime.Now,
                SocialMediaLinks = "SocialMedia",
                NumberOfPosts = 10
            };

            var handler = new DeleteAuthorHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
            var request = new DeleteAuthor(authorId);

            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(authorId)).ReturnsAsync(author);
            _unitOfWorkMock.Setup(uow => uow.Authors.DeleteAsync(author.Id)).ReturnsAsync(author);
           

            _mapperMock.Setup(m => m.Map<AuthorDto>(author)).Returns(authorDto);

            // Act
            var result = await handler.Handle(request, default);

            // Assert   

            _unitOfWorkMock.Verify(uow => uow.BeginTransactionAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Authors.DeleteAsync(author.Id), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(authorId, result.Id);
        }

        [Fact]
        public async Task Handle_InvalidOrderId_ThrowsException()
        {
            // Arrange
            int orderId = 1;

            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(orderId)).ReturnsAsync((Author)null);

            var handler = new DeleteOrderHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
            var request = new DeleteOrder(orderId);

            // Act & Assert
            await Assert.ThrowsAsync<NullReferenceException>(async () => await handler.Handle(request, CancellationToken.None));

            _unitOfWorkMock.Verify(uow => uow.BeginTransactionAsync(), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.Orders.DeleteAsync(It.IsAny<int>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.CommitTransactionAsync(), Times.Never);

        }
    }
}
