using Application.UnitTests.Helpers;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTests.CommandTests.ProductCommandTests
{
    public class CreateProductHandlerTest : BaseCommandHandlerTest
    {
        [Fact]
        public async Task Create_Product_Command_Should_Create_Returns_ProductDto()
        {
            var command = new CreateProduct
            {
                Title = "Title",    
                Description = "Description",
                CategoryID = 1,
                AuthorId = 3,
                Quantity = 2,   
                Price = 100,
            };
        }
    }
}
