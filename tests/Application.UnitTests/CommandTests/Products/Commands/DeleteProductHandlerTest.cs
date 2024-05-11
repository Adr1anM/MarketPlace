using Application.UnitTests.Helpers;
using Azure.Core;
using MarketPlace.Application.Abstractions.Repositories;
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
using System.Threading.Tasks;

namespace Application.UnitTests.CommandTests.ProductCommandTests
{
    public class DeleteProductHandlerTest : BaseCommandHandlerTest
    {
        private readonly DeleteProductHandler _handler;
        public DeleteProductHandlerTest()
        {
            _handler = new DeleteProductHandler(
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerFactoryMock.Object);
        }

        private Product CreateTestProduct(int productId)
        {
            return new Product
            {
                Id = productId,
                Title = "Title",
                Description = "Descriprion",
                CategoryID = 2,
                AuthorId = 3,
                Quantity = 7,
                Price = 20000,
                CreatedDate = DateTime.UtcNow,
            };
        }
        private ProductDto CreateTestProductDto(int productId)
        {
            return new ProductDto
            {
                Id = productId,
                Title = "Title",
                Description = "Descriprion",
                CategoryID = 2,
                AuthorId = 3,
                Quantity = 7,
                Price = 20000,
                CreatedDate = DateTime.UtcNow,
            };
        }

        [Fact]
        public async Task Delete_Product_Handler_Should_Delete_And_Return_ProductDto()
        {
            //Arrange
            int productId = 1;
            var product = CreateTestProduct(productId);
            var productDto = CreateTestProductDto(productId);

            var command = new DeleteProduct(productId);
            _unitOfWorkMock.Setup(uow => uow.Products.GetByIdAsync(productId)).ReturnsAsync(product);
            _unitOfWorkMock.Setup(uow => uow.Products.DeleteAsync(product.Id)).ReturnsAsync(product);
            _mapperMock.Setup(m => m.Map<ProductDto>(product)).Returns(productDto);

            var result = await _handler.Handle(command, default);

            _unitOfWorkMock.Verify(uow => uow.Products.DeleteAsync(product.Id), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Once);

            Assert.NotNull(result);
            Assert.Equal(productId, result.Id);
        }

        [Fact]
        public async Task Delete_NonExisting_Product_Throws_EntityNotFoundException()
        {
            //Arrange
            int productId = 1;
            _unitOfWorkMock.Setup(uow => uow.Products.GetByIdAsync(productId)).ReturnsAsync((Product)null);
            var request = new DeleteProduct(productId);

            // Act & Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(async () => await _handler.Handle(request, CancellationToken.None));

            _unitOfWorkMock.Verify(uow => uow.Products.DeleteAsync(It.IsAny<int>()), Times.Never);
            _unitOfWorkMock.Verify(uow => uow.SaveAsync(CancellationToken.None), Times.Never);
        }

    }
}
