using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Responses;
using MarketPlace.Application.App.Orders.Responses;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Domain.Models;
using MarketPlace.Domain.Models.Auth;
using MarketPlace.Domain.Models.Enums;
using MarketPlace.WebUI.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUi.Tests.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace WebUi.Tests.ControllersTests.IntegrationTests.Authors
{
    public class AuthorsControllerTests : IClassFixture<ControllerTestFixture>
    {
        private readonly ControllerTestFixture _fixture;
        private readonly AuthorsController _controller;
        public AuthorsControllerTests(ControllerTestFixture fixture)
        {
            _fixture = fixture;
            _controller = new AuthorsController(_fixture.Mediator);
        }


        [Fact]
        public async Task Create_Author_Should_Return_OkResult()
        {
            //Arrange
            var context = _fixture.GetContext();
            await UsersHelper.SeedData(_fixture.serviceProvider);

            var command = new CreateAuthor(1, "Biography", "Country", DateTime.Now, "SocialMedia", 10);
            // Act
            var result = await _controller.CreateAuthor(command);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var authorDto = Assert.IsType<AuthorDto>(createdResult.Value);
            Assert.Equal(command.Biography, authorDto.Biography);

            SeedHelper.CleanDatabase(context);
            await UsersHelper.ClearUsers(_fixture.UserManager);

        }

        [Fact]
        public async Task Get_Author_ByInvalideId_Returns_NotFoundResponse()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedAuthors(context);
            await UsersHelper.SeedData(_fixture.serviceProvider);
              

            //Act
            var result = await _controller.GetAuthorById(7);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            SeedHelper.CleanDatabase(context);
            await UsersHelper.ClearUsers(_fixture.UserManager);

        }

        [Fact]
        public async Task Get_Author_ById_Returns_OkResponse()
        {

            // Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedAuthors(context);
            await UsersHelper.SeedData(_fixture.serviceProvider);
               

            var author = new AuthorDto
            {
                Id = 1,
                UserId = 2,
                Biography = "Biography",
                Country = "Country",
                BirthDate = DateTime.Now,
                SocialMediaLinks = "SocialMediaLinks",
                NumberOfPosts = 3
            };
            
            // Act
            var result = await _controller.GetAuthorById(author.Id);

            // Assert
            var createdResult = Assert.IsType<OkObjectResult>(result);
            var authorDtoResult = Assert.IsType<AuthorDto>(createdResult.Value);

            Assert.Multiple(() =>
            {
                Assert.Equal(author.Id, authorDtoResult.Id);
                Assert.Equal(author.UserId, authorDtoResult.UserId);
                Assert.Equal(author.Biography, authorDtoResult.Biography);
                Assert.Equal(author.Country, authorDtoResult.Country);
                Assert.Equal(author.SocialMediaLinks, authorDtoResult.SocialMediaLinks);
                Assert.Equal(author.NumberOfPosts, authorDtoResult.NumberOfPosts);
              
            });

            SeedHelper.CleanDatabase(context);
            await UsersHelper.ClearUsers(_fixture.UserManager);

        }


        [Fact]
        public async Task Update_Author_Should_Return_OkResult_And_AuthorDto()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedAuthors(context);
            await UsersHelper.SeedData(_fixture.serviceProvider);
           

            var command = new UpdateAuthor(2, 3, "biography", "moldova", DateTime.Now, "SocialMedia", 10);
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
            //Act
            var result = await _controller.UpdateAuthor(command);

            //Assert
            var responseResult = Assert.IsType<OkObjectResult>(result);
            var authorDtoResult = Assert.IsType<AuthorDto>(responseResult.Value);

            Assert.Multiple(() =>
            {
                Assert.Equal(authorDto.Id, authorDtoResult.Id);
                Assert.Equal(authorDto.UserId, authorDtoResult.UserId);
                Assert.Equal(authorDto.Biography, authorDtoResult.Biography);
                Assert.Equal(authorDto.Country, authorDtoResult.Country);
                Assert.Equal(authorDto.SocialMediaLinks, authorDtoResult.SocialMediaLinks);
                Assert.Equal(authorDto.NumberOfPosts, authorDtoResult.NumberOfPosts);

            });

            SeedHelper.CleanDatabase(context);
            await UsersHelper.ClearUsers(_fixture.UserManager);

        }

        [Fact]
        public async Task Update_InvalidAuthor_Should_Return_NotFoundResult()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedAuthors(context);
            await UsersHelper.SeedData(_fixture.serviceProvider);
            
            int authorId = 21;

            var command = new UpdateAuthor(authorId, 3, "biography", "moldova", DateTime.Now, "SocialMedia", 10);

            //Act
            var result = await _controller.UpdateAuthor(command);

            //Assert
            var responseReusult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("No such Author", responseReusult.Value);
            SeedHelper.CleanDatabase(context);
            await UsersHelper.ClearUsers(_fixture.UserManager);

        }

        [Fact]
        public async Task Delete_Author_Returns_OkResponse_And_Author_Dto()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedAuthors(context);
            await UsersHelper.SeedData(_fixture.serviceProvider);

            int authorId = 2;

            var authorDto = new AuthorDto
            {
                Id = 2,
                UserId = 2,
                Biography = "Biography2",
                Country = "Germany",
                BirthDate = DateTime.Now,
                SocialMediaLinks = "SocialMedia2",
                NumberOfPosts = 2
            };

            //Act
            var result = await _controller.DeleteAuthor(authorId);

            //Assert
            var response = Assert.IsType<OkObjectResult>(result);
            var responseObject = Assert.IsType<AuthorDto>(response.Value);

            Assert.Multiple(() =>
            {
                Assert.Equal(authorDto.Biography, responseObject.Biography);
                Assert.Equal(authorDto.Country, responseObject.Country);
                Assert.Equal(authorDto.SocialMediaLinks, responseObject.SocialMediaLinks);
                Assert.Equal(authorDto.NumberOfPosts, responseObject.NumberOfPosts);
                Assert.Equal(authorDto.UserId, responseObject.UserId);
            });

            SeedHelper.CleanDatabase(context);
            await UsersHelper.ClearUsers(_fixture.UserManager);
        }


        [Fact]
        public async Task Delete_InvalideAuthor_Returns_NotFoundResponse()
        {
            //Arrange
            var context = _fixture.GetContext();
            SeedHelper.SeedAuthors(context);
            await UsersHelper.SeedData(_fixture.serviceProvider);

            int authorId = 5;

          

            //Act & Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await _controller.DeleteAuthor(authorId));
            Assert.Equal("No such Author found", exception.Message);

            SeedHelper.CleanDatabase(context);
            await UsersHelper.ClearUsers(_fixture.UserManager);
        }

        [Fact]
        public async Task GetAllOrders_Returns_OkObjectResult_And_ListOf_OrdersDto()
        {
            //Arrange
            var context = _fixture.GetContext();
            var expectedAuthorsList = SeedHelper.GetAllSeededAuthors().ToList();
            SeedHelper.SeedAuthors(context);

            //Act
            var result = await _controller.GetAllAuthors();

            //Assert 
            var response = Assert.IsType<OkObjectResult>(result);
            var actualAuthorsList = Assert.IsAssignableFrom<IEnumerable<AuthorDto>>(response.Value);

            var sortedActualAuthorList = actualAuthorsList.OrderBy(x => x.Id).ToList();

            Assert.Equal(expectedAuthorsList.Count(), actualAuthorsList.Count());
            Assert.Multiple(() =>
            {
                for (int i = 0; i < expectedAuthorsList.Count; i++)
                {
                    var expectedAuthor = expectedAuthorsList[i];
                    var actualAuthor = sortedActualAuthorList[i];

                    Assert.Equal(expectedAuthor.Id, actualAuthor.Id);
                    Assert.Equal(expectedAuthor.UserId, actualAuthor.UserId);
                    Assert.Equal(expectedAuthor.Biography, actualAuthor.Biography);
                    Assert.Equal(expectedAuthor.SocialMediaLinks, actualAuthor.SocialMediaLinks);
                    Assert.Equal(expectedAuthor.NumberOfPosts, actualAuthor.NumberOfPosts);
                }
            });
            SeedHelper.CleanDatabase(context);
        }

        [Fact]
        public async Task GetAllOrders_Returns_NotFoundObjectResult()
        {
            //Arrange
            var context = _fixture.GetContext();

            //Act
            var result = await _controller.GetAllAuthors();

            var response = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("There are no authors", response.Value);
            SeedHelper.CleanDatabase(context);
        }
    }
}
