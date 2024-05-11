using Moq;
using AutoMapper;
using Microsoft.Extensions.Logging;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Application.Orders.Delete;
using Application.UnitTests.Helpers;
using MarketPlace.Application.Exceptions;


namespace Application.UnitTests.CommandTests.Authors.Comands
{
    public class DeleteAuthorHandleTest : BaseCommandHandlerTest
    {
        private readonly DeleteAuthorHandler _handler;
        public DeleteAuthorHandleTest()
        {
            _handler = new DeleteAuthorHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerFactoryMock.Object);
        }

        private Author CreateTestAuthor(int authorId)
        {
            return new Author
            {
                Id = authorId,
                UserId = 2,
                Biography = "Biography",
                Country = "Country",
                BirthDate = DateTime.Now,
                SocialMediaLinks = "SocialMedia",
                NumberOfPosts = 10
            };
        }

        private AuthorDto CreateTestAuthorDto(int authorId)
        {
           return new AuthorDto
            {
                Id = authorId,
                UserId = 2,
                Biography = "Biography",
                Country = "Country",
                BirthDate = DateTime.Now,
                SocialMediaLinks = "SocialMedia",
                NumberOfPosts = 10
            };
        }

        [Fact]
        public async Task Handle_ValidAuthorId_DeletesAuthorAndReturnsAuthorDto()
        {
            // Arrange
            int authorId = 1;
            var author = CreateTestAuthor(authorId);
            var authorDto = CreateTestAuthorDto(authorId);
        

            var request = new DeleteAuthor(authorId);

            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(authorId)).ReturnsAsync(author);
            _unitOfWorkMock.Setup(uow => uow.Authors.DeleteAsync(author.Id)).ReturnsAsync(author);
            _mapperMock.Setup(m => m.Map<AuthorDto>(author)).Returns(authorDto);

            // Act
            var result = await _handler.Handle(request, default);

            // Assert   
            _unitOfWorkMock.Verify(uow => uow.Authors.DeleteAsync(author.Id), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);
                
            Assert.NotNull(result);
            Assert.Equal(authorId, result.Id);
        }

        [Fact]
        public async Task Handle_InvalidAuthorId_ThrowsException()
        {
            // Arrange
            int authorId = 1;

            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(authorId)).ReturnsAsync((Author)null);    
            var request = new DeleteAuthor(authorId);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            _unitOfWorkMock.Verify(uow => uow.Authors.DeleteAsync(It.IsAny<int>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Never);

        }
    }
}
