using Application.UnitTests.Helpers;
using AutoMapper;
using Azure.Core;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Exceptions;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Application.Products.Delete;
using MarketPlace.Domain.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UnitTests.CommandTests.Products.Commands
{
    public class UpdateProductHandlerTest : BaseCommandHandlerTest
    {
        private readonly UpdateProductHandler _handler;
        public UpdateProductHandlerTest()
        {
            _handler = new UpdateProductHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerFactoryMock.Object);
        }

        private Product CreateTestProduct(UpdateProduct command)
        {
            return new Product
            {
                Id = command.Id,
                Title = command.Title,
                Description = command.Description,
                CategoryID = command.CategoryID,
                AuthorId = command.AuthorId,
                Quantity = command.Quantity,
                Price = command.Price,
                CreatedDate = command.CreatedDate,
            };
        }
        private ProductDto CreateTestProductDto(UpdateProduct command)
        {
            return new ProductDto
            {
                Id = command.Id,
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
        public async Task UpdateProduct_Command_Should_Update_ReturnsProductDto()
        {
            var command = new UpdateProduct(1, "Title", "Description", 1, 3, 2, 100, DateTime.UtcNow); 

            var initialProduct = new Product
            {
                Id = command.Id,
                Title = command.Title,
                Description = "asdasdsa",
                CategoryID = 4,
                AuthorId = command.AuthorId,
                Price = 200,
                Quantity = 4,
                CreatedDate = command.CreatedDate,

            };
            var updatedProduct = CreateTestProduct(command);
            var productDto = CreateTestProductDto(command);


            _unitOfWorkMock.Setup(uaw => uaw.Products.GetByIdAsync(command.Id)).ReturnsAsync(initialProduct);

            _mapperMock.Setup(m => m.Map<ProductDto>(initialProduct)).Returns(productDto);
            _mapperMock.Setup(m => m.Map(command, initialProduct)).Returns(updatedProduct);
            

            var result = await _handler.Handle(command, default);

            // Assert   
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);

            
            Assert.Multiple(() =>
            {
                Assert.Equal(command.Id, result.Id);
                Assert.Equal(command.Title, result.Title);
                Assert.Equal(command.Description, result.Description);
                Assert.Equal(command.Price, result.Price);
            });
        }

        [Fact]
        public async Task Update_NonExisting_Product_Throw_EntityNotFoundException()
        {
            //Arrange
            var command = new UpdateProduct(1, "Title", "Description", 1, 3, 2, 100, DateTime.UtcNow);
            _unitOfWorkMock.Setup(uow => uow.Products.GetByIdAsync(command.Id)).ReturnsAsync((Product)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(command, CancellationToken.None));
            Assert.Equal($"Entity of type '{typeof(Product).Name}' with ID '{command.Id}' not found.", exception.Message);
        } 
        
        [Fact]
        public async Task Update_Product_Throw_AutoMapperMappingException()
        {
            //Arrange
            var command = new UpdateProduct(1, "Title", "Description", 1, 3, 2, 100, DateTime.UtcNow);
            var product = new Product();
            _unitOfWorkMock.Setup(uow => uow.Products.GetByIdAsync(command.Id)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map(It.IsAny<UpdateProduct>(), It.IsAny<Product>())).Throws<AutoMapperMappingException>();

            // Act & Assert
            await Assert.ThrowsAsync<AutoMapperMappingException>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}
