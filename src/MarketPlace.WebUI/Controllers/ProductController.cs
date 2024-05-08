﻿using MarketPlace.Application.App.Products.Commands;
using MarketPlace.Application.App.Products.GetPagedResult;
using MarketPlace.Application.Products.Delete;
using MarketPlace.Application.Products.GetPagedResult;
using MarketPlace.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MarketPlace.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

       
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProduct command)
        {
            var productDto = await _mediator.Send(command);
            return Ok(productDto);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdatePrudoct command)
        {
            try
            {
                var updateProducDto = await _mediator.Send(command);
                return Ok(updateProducDto);
            }
            catch(Exception ex) 
            {
                return NotFound(ex.Message);            
            }
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


        [HttpGet("all")]
        public async Task<IActionResult> GetAllProducts()
        {
            var producs = await _mediator.Send(new GetAllProductsQuerry());

            return Ok(producs);
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedProducts(GetPagedProductsQuerry command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product =  await _mediator.Send(new DeleteProduct(id));
            if(product == null)
            {
                return NotFound($"Product with Id{id} does not exist");
            }
            return Ok("The Product was deleted successfuly");  
        }

    }
}