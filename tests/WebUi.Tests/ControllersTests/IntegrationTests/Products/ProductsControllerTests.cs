/*using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Update;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Domain.Models;
using MarketPlace.WebUI.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUi.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using MarketPlace.Infrastructure.Persistance.Context;
using Microsoft.AspNetCore.Identity;
using MarketPlace.Application.App.Products.Commands;
using System.Net.Http.Json;
using MarketPlace.Application.Paints.Responses;
using System.Net;
using Azure;
using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.Products.Delete;
using MarketPlace.Application.Products.GetPagedResult;

namespace WebUi.Tests.ControllersTests.IntegrationTests.Products
{
    public class ProductsControllerTests : IDisposable
    {
        private readonly BaseWebApplicationFactory _factory;
        private readonly HttpClient _client;

        public ProductsControllerTests()
        {
            _factory = new BaseWebApplicationFactory();
            _client = _factory.CreateClient();
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            SeedHelper.SeedDatabase(context);
        }

        [Fact]
        public async Task CreateProduct_ReturnsOkResult_And_ProductDto()
        {

            //Arrange
            var command = new CreateProduct("Title", "Description", 1, 3, 2, 100, DateTime.UtcNow);

            // Act
            var result = await _client.PostAsJsonAsync("api/Product", command);
            var createdProduct = await result.Content.ReadFromJsonAsync<ProductDto>();

            Assert.NotNull(createdProduct);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                Assert.Equal(command.Title, createdProduct.Title);
                Assert.Equal(command.Description, createdProduct.Description);
                Assert.Equal(command.Price, createdProduct.Price);
                Assert.Equal(command.Quantity, createdProduct.Quantity);
            });

        }


        [Fact]
        public async Task Create_Product_WithNonExisting_CategoryId_ReturnsNotFound()
        {
            var command = new CreateProduct("Title", "Description", 1, 10, 2, 100, DateTime.UtcNow);

            var response = await _client.PostAsJsonAsync("api/Product", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }


        [Fact]
        public async Task Create_Product_WithInvalidParams_ReturnsBadRequest()
        {
            var command = new CreateProduct("Title", "Description", 0, 0, 2, 100, DateTime.UtcNow);

            var response = await _client.PostAsJsonAsync("api/Product", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }

        [Fact]
        public async Task Create_Product_WithNonExisting_AuthorId_ReturnsNotFound()
        {
            var command = new CreateProduct("Title", "Description", 1, 10, 2, 100, DateTime.UtcNow);

            var response = await _client.PostAsJsonAsync("api/Product", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }


        [Fact]
        public async Task Update_Product_ReturnsOkResult_And_ProductDto()
        {
            //Arrange
            var command = new UpdateProduct(1, "Titlurile", "Descriere", 1, 3, 2, 10000, DateTime.UtcNow);
            //Act
            var response = await _client.PutAsJsonAsync("api/Product", command);
            var updatedProduct = await response.Content.ReadFromJsonAsync<ProductDto>();

            //Assert
            Assert.NotNull(updatedProduct);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(command.Title, updatedProduct.Title);
                Assert.Equal(command.Description, updatedProduct.Description);
                Assert.Equal(command.Price, updatedProduct.Price);
                Assert.Equal(command.Quantity, updatedProduct.Quantity);
            });
        }

        [Fact]
        public async Task UpdateProduct_InvalidId_Returns_NotFound()
        {
            //Arrange
            int productId = 20;
            var command = new UpdateProduct(productId, "Titlurile", "Descriere", 2, 3, 2, 10000, DateTime.UtcNow);
            //Act
            var response = await _client.PutAsJsonAsync("api/Product", command);
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.NotEmpty(content);
            });
        }

        [Fact]
        public async Task Update_Product_WithInvalidParams_ReturnsBadRequest()
        {
            var command = new UpdateProduct(21,"Title", "Description", 0, 0, 2, 100, DateTime.UtcNow);

            var response = await _client.PutAsJsonAsync("api/Product", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }


        [Fact]
        public async Task DeleteProduct_Returns_OkResult_AndProductDto()
        {
            //Arrange
            int productId = 1;
            var command = new DeleteProduct(productId);

            //Act
            var response = await _client.DeleteAsync($"api/Product/{productId}");
            var result = await response.Content.ReadAsStringAsync();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var deletedProduct = context.Products.Find(productId);

            // Assert
            Assert.NotEmpty(result);
            Assert.Multiple(() =>
            {
                
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal("The Product was deleted successfuly", result);
                Assert.Null(deletedProduct);
            });

        }    
        
        
        [Fact]
        public async Task DeleteProduct_InvalidId_Returns_NotFound()
        {
            //Arrange
            int productId = 12;
            var command = new DeleteProduct(productId);

            //Act
            var response = await _client.DeleteAsync($"api/Product/{productId}");
            var result = await response.Content.ReadAsStringAsync();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var deletedProduct = context.Products.Find(productId);

            // Assert
            Assert.NotEmpty(result);
            Assert.Multiple(() =>
            {       
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.Null(deletedProduct);
            });

        }

        [Fact]
        public async Task GetAllProducts_ReturnsAllProductsDto_OkResult()
        {     
            //Act
            var response = await _client.GetAsync("api/Product/all");
            var products = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var existingProducts = context.Products.ToList();

            //Assert
            Assert.NotNull(products);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                foreach (var existingProduct in existingProducts)
                {
                    var productDto = products.FirstOrDefault(p => p.Id == existingProduct.Id);
                    Assert.NotNull(productDto); 

                    Assert.Equal(existingProduct.Price, productDto.Price);
                    Assert.Equal(existingProduct.CategoryID, productDto.CategoryID);
                    Assert.Equal(existingProduct.Quantity, productDto.Quantity);
                    Assert.Equal(existingProduct.Title, productDto.Title);
                    Assert.Equal(existingProduct.AuthorId, productDto.AuthorId);               
                }

            });
        }

        [Fact]
        public async Task GetProductById_Returns_ProductDto()
        {
            //Arrange
            int productId = 1;

            //Act
            var response = await _client.GetAsync($"api/Product/{productId}");
            var product = await response.Content.ReadFromJsonAsync<ProductDto>();

            //Assert
            Assert.NotNull(product );
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(productId, product.Id);
            });
        }

        [Fact]
        public async Task GetProductBy_InvalidId_Returns_NotFoundResponse()
        {
            //Arrange
            int productId = 8;

            //Act
            var response = await _client.GetAsync($"api/Product/{productId}");

            //Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }

        [Fact]
        public async Task GetPaged_Products_ConcreteNumber_of_Products()
        {
            var command = new GetPagedProductsQuerry(1, 3);

            var response = await _client.PostAsJsonAsync("api/Product/paged", command);
            var pagedProducts = await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();


            Assert.NotNull(pagedProducts);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(command.PageSize, pagedProducts.Count());

        }

        public void Dispose()
        {
            using var scope = _factory.Services.CreateScope();
             var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            context.Database.EnsureDeleted();

            _factory.Dispose();
            _client.Dispose();
        }


    }
}
*/