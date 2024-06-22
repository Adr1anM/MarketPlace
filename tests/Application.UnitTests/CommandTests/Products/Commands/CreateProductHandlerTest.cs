/*using Application.UnitTests.Helpers;
using Azure.Core;
using MarketPlace.Application.Abstractions;
using MarketPlace.Application.Abstractions.Repositories;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.UnitTests.CommandTests.Products.Commands
{
    public class CreateProductHandlerTest : BaseCommandHandlerTest
    {
        private readonly Mock<IGenericRepository<Category>> _categoryGenericRepo;
        private readonly CreateProductHandler _handler;
        public CreateProductHandlerTest()
        {
            _categoryGenericRepo = new Mock<IGenericRepository<Category>>();
            _handler = new CreateProductHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerFactoryMock.Object);
        }

        private Product CreateTestProduct(CreateProduct command)
        {
            return new Product
            {
                Id = 1,
                Title = command.Title,
                Description = command.Description,
                CategoryID = command.CategoryID,
                AuthorId = command.AuthorId,
                Quantity = command.Quantity,
                Price = command.Price,
                CreatedDate = command.CreatedDate,
            };
        }
        private ProductDto CreateTestProductDto(CreateProduct command)
        {
            return new ProductDto
            {
                Id = 1,
                Title = command.Title,
                Description = command.Description,
                CategoryID = command.CategoryID,
                AuthorId = command.AuthorId,
                Quantity = command.Quantity,
                Price = command.Price,
                CreatedDate = command.CreatedDate,
            };
        }

        [Fact]
        public async Task Create_Product_Command_Should_Create_Returns_ProductDto()
        {
            //Arrange
            var command = new CreateProduct("Title", "Description", 1, 3, 2, 100, DateTime.UtcNow);
            var product = CreateTestProduct(command);
            var productDto = CreateTestProductDto(command);

            var mockedCategory = new Category { Id = 1, Name = "MockCategory" };
            var author = new Author
            {
                Id = 3,
                UserId = 2,
                Biography = "Biography",
                Country = "Country",
                BirthDate = DateTime.Now,
                SocialMediaLinks = "SocialMedia",
                NumberOfPosts = 10
            };

            _unitOfWorkMock.Setup(uow => uow.GetGenericRepository<Category>()).Returns(_categoryGenericRepo.Object);
            _categoryGenericRepo.Setup(repo => repo.GetByIdAsync(command.CategoryID)).ReturnsAsync(mockedCategory);
            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(command.AuthorId)).ReturnsAsync(author);
            _mapperMock.Setup(m => m.Map<Product>(command)).Returns(product);
            _unitOfWorkMock.Setup(uow => uow.Products.AddAsync(product)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);
            _unitOfWorkMock.Setup(uow => uow.SaveAsync(CancellationToken.None)).Returns(Task.CompletedTask);
          
            //Act
            var result = await _handler.Handle(command, default);

            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once); 
            _unitOfWorkMock.Verify(uow => uow.GetGenericRepository<Category>(), Times.Once);

            Assert.NotNull(result);
            Assert.Multiple(() =>
            {
                Assert.Equal(author.Id, result.AuthorId);
                Assert.Equal(mockedCategory.Id, result.CategoryID);
                Assert.Equal(command.Title, result.Title);
                Assert.Equal(command.Description, result.Description);
                Assert.Equal(command.Price, result.Price);
            });
        }


        [Fact]
        public async Task Create_Product_UnexistingCategory_Throws_EntityNotFoundException()
        {
            var command = new CreateProduct("Title", "Description", 1, 3, 2, 100, DateTime.UtcNow);
            var notFoundException = new EntityNotFoundException(typeof(Category), command.CategoryID);
            _unitOfWorkMock.Setup(uow => uow.GetGenericRepository<Category>()).Returns(_categoryGenericRepo.Object);
            _categoryGenericRepo.Setup(repo => repo.GetByIdAsync(command.CategoryID)).ThrowsAsync(notFoundException);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() =>  _handler.Handle(command, CancellationToken.None));
        }


        [Fact]
        public async Task Create_Product_UnexistingAuthor_Throws_EntityNotFoundException()
        {
            var command = new CreateProduct("Title", "Description", 1, 10, 2, 100, DateTime.UtcNow);
            var notFoundException = new EntityNotFoundException(typeof(Author), command.CategoryID);
            var mockedCategory = new Category { Id = 1, Name = "MockCategory" };

            _unitOfWorkMock.Setup(uow => uow.GetGenericRepository<Category>()).Returns(_categoryGenericRepo.Object);
            _categoryGenericRepo.Setup(repo => repo.GetByIdAsync(command.CategoryID)).ReturnsAsync(mockedCategory);
            _unitOfWorkMock.Setup(uow => uow.Authors.GetByIdAsync(command.AuthorId)).ReturnsAsync((Author)null);


            // Act & Assert
            var exceptionResult = await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Entity of type '{typeof(Author).Name}' with ID '{command.AuthorId}' not found.", exceptionResult.Message);

            _loggerMock.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId.Id == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == $"Entity of type '{typeof(Author).Name}' with ID '{command.AuthorId}' not found."),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

    }

}*/