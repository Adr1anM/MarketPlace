using MarketPlace.Application.App.Categories.Querries;
using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.App.Products.GetPagedResult;
using MarketPlace.Application.App.Products.Querries;
using MarketPlace.Application.Common.Models;
using MarketPlace.Application.Products.Delete;
using MarketPlace.Application.Products.GetPagedResult;
using MarketPlace.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketPlace.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProduct command)
        {
            var productDto = await _mediator.Send(command);
            return Ok(productDto);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> UpdateProduct([FromForm]UpdateProduct command)
        {
            var updateProducDto = await _mediator.Send(command);
            return Ok(updateProducDto);
        }
          
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var productDto = await _mediator.Send(new GetProductByIdQuerry(id));
            if(productDto == null)
            {
                return NotFound("Such Product does not exts");
            }

            return Ok(productDto);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var producs = await _mediator.Send(new GetAllProductsQuerry());
            return Ok(producs);
        }

        [HttpGet("by-author/{authorId}")]
        public async Task<IActionResult> GetAllProductsByAuthId(int authorId)
        {
            var producs = await _mediator.Send(new GetAllProductsByIdQuerry(authorId));
            return Ok(producs);
        }

        [HttpPost("paged")]
        public async Task<IActionResult> GetPagedProducts(PagedRequest request)
        {
            var result = await _mediator.Send(new GetPagedProductsQuerry(request));
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product =  await _mediator.Send(new DeleteProduct(id));
            return Ok(product);  
        }


       
    }
}
