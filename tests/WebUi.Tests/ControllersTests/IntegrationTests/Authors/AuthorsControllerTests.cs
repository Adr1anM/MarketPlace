using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Paints.Responses;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.Infrastructure.Persistance.Context;
using MarketPlace.WebUI.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebUi.Tests.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using MarketPlace.Application.Products.Delete;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Application.Orders.Update;

namespace WebUi.Tests.ControllersTests.IntegrationTests.Authors
{
    public class AuthorsControllerTests : IDisposable
    {
        private readonly BaseWebApplicationFactory _factory;
        private readonly HttpClient _client;
        public AuthorsControllerTests()
        {
            _factory = new BaseWebApplicationFactory();
            _client = _factory.CreateClient();
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            SeedHelper.SeedDatabase(context);
        }

        [Fact]
        public async Task CreateAuthor_ReturnsOkResult_And_AuthorDto()
        {

            //Arrange
            var command = new CreateAuthor(1, "Biography", "Country", DateTime.Now, "SocialMedia", 10);

            // Act
            var result = await _client.PostAsJsonAsync("api/Authors", command);
            var createdAuthor = await result.Content.ReadFromJsonAsync<AuthorDto>();

            Assert.NotNull(createdAuthor);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, result.StatusCode);
                Assert.Equal(command.Country, createdAuthor.Country);
                Assert.Equal(command.Biography, createdAuthor.Biography);
                Assert.Equal(command.Country, createdAuthor.Country);
                Assert.Equal(command.SocialMediaLinks, createdAuthor.SocialMediaLinks);
                Assert.Equal(command.NumberOfPosts, createdAuthor.NumberOfPosts);
            });       
        }

        [Fact]
        public async Task Create_Author_WithNonExisting_UserId_ReturnsNotFound()
        {
            var command = new CreateAuthor(20, "Biography", "Country", DateTime.Now, "SocialMedia", 10);

            var response = await _client.PostAsJsonAsync("api/Authors", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            });
        }


        [Fact]
        public async Task Create_Author_WithInvalidParams_ReturnsBadRequest()
        {
            var command = new CreateAuthor(0, "Biography", "Country", DateTime.Now, "SocialMedia", 0);

            var response = await _client.PostAsJsonAsync("api/Authors", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });

        }

        [Fact]
        public async Task Update_Author_ReturnsOkResult_And_AuthorDto()
        {
            //Arrange
            var command = new UpdateAuthor(2, 3, "biography", "moldova", DateTime.Now, "SocialMedia", 10);
            //Act
            var response = await _client.PutAsJsonAsync("api/Authors", command);
            var updatedOrder = await response.Content.ReadFromJsonAsync<AuthorDto>();

            //Assert
            Assert.NotNull(updatedOrder);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(command.Id, updatedOrder.Id);
                Assert.Equal(command.UserId, updatedOrder.UserId);
                Assert.Equal(command.SocialMediaLinks, updatedOrder.SocialMediaLinks);
                Assert.Equal(command.Country, updatedOrder.Country);
                Assert.Equal(command.Biography, updatedOrder.Biography);
                Assert.Equal(command.NumberOfPosts, updatedOrder.NumberOfPosts);
            });
        }

        [Fact]
        public async Task UpdateAuthor_InvalidId_Returns_NotFound()
        {
            //Arrange
            int authorId = 20;
            var command = new UpdateAuthor(authorId, 3, "biography", "moldova", DateTime.Now, "SocialMedia", 10);
            //Act
            var response = await _client.PutAsJsonAsync("api/Authors", command);
            var content = await response.Content.ReadAsStringAsync();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.NotEmpty(content);
            });
        }


        [Fact]
        public async Task Update_Author_WithInvalidParams_ReturnsBadRequest()
        {
            var command = new UpdateAuthor(0, 0, "biography", "moldova", DateTime.Now, "SocialMedia", 10);

            var response = await _client.PutAsJsonAsync("api/Authors", command);
            var content = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            });
        }


        [Fact]
        public async Task DeleteAuthor_Returns_OkResult_AndAuthorDto()
        {
            //Arrange
            int authorId = 1;
            var command = new DeleteAuthor(authorId);

            //Act
            var response = await _client.DeleteAsync($"api/Authors/{authorId}");
            var result = await response.Content.ReadFromJsonAsync<OrderDto>();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var deletedAuthor = context.Authors.Find(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.Multiple(() =>
            {

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(authorId, result.Id);
                Assert.Null(deletedAuthor);
            });

        }


        [Fact]
        public async Task DeleteAuthor_InvalidId_Returns_NotFound()
        {
            //Arrange
            int authorId = 12;
            var command = new DeleteAuthor(authorId);


            //Act
            var response = await _client.DeleteAsync($"api/Authors/{authorId}");
            var result = await response.Content.ReadAsStringAsync();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var deletedProduct = context.Authors.Find(authorId);

            // Assert
            Assert.NotEmpty(result);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
                Assert.Null(deletedProduct);
            });

        }


        [Fact]
        public async Task GetAllAuthors_ReturnsAllAuthorsDto_OkResult()
        {
            //Act
            var response = await _client.GetAsync("api/Authors/all");
            var authors = await response.Content.ReadFromJsonAsync<IEnumerable<AuthorDto>>();

            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ArtMarketPlaceDbContext>();
            var existingAuthors = context.Authors.ToList();

            //Assert
            Assert.NotNull(authors);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                foreach (var existingAuthor in existingAuthors)
                {
                    var authorDto = authors.FirstOrDefault(p => p.Id == existingAuthor.Id);
                    Assert.NotNull(authorDto);
                    Assert.Equal(existingAuthor.UserId, authorDto.UserId);
                    Assert.Equal(existingAuthor.SocialMediaLinks, authorDto.SocialMediaLinks);
                    Assert.Equal(existingAuthor.Country, authorDto.Country);
                    Assert.Equal(existingAuthor.Biography, authorDto.Biography);
                    Assert.Equal(existingAuthor.NumberOfPosts, authorDto.NumberOfPosts);
                }
            });
        }

        [Fact]
        public async Task GetAuthorById_Returns_AuthorDto()
        {
            //Arrange
            int authorId = 1;

            //Act
            var response = await _client.GetAsync($"api/Authors/{authorId}");
            var author = await response.Content.ReadFromJsonAsync<Author>();

            //Assert
            Assert.NotNull(author);
            Assert.Multiple(() =>
            {
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(authorId, author.Id);
            });
        }



        [Fact]
        public async Task GetAuthorBy_InvalidId_Returns_NotFoundResponse()
        {
            //Arrange
            int authorId = 13;

            //Act
            var response = await _client.GetAsync($"api/Authors/{authorId}");

            //Assert       
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
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
