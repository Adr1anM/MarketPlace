using MarketPlace.Application.App.ShoppingCart.Commands;
using MarketPlace.Application.App.ShoppingCarts.CartModels;
using MarketPlace.Application.App.ShoppingCarts.Commands;
using MarketPlace.Application.App.ShoppingCarts.Querries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ShoppingCartsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddToshoppingCart(AddCartModel model)
        {
            var result = await _mediator.Send(new AddToCart(model));
            return Ok(result);  
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveShoppingCart(int userId, int productId)
        {
            var result = await _mediator.Send(new RemoveFromCart(userId, productId));
            return Ok(result);
        } 

        [HttpPut]
        public async Task<IActionResult> UpdateShoppingCart(AddCartModel model)
        {
            var result = await _mediator.Send(new UpdateCartItem(model));
            return Ok(result);
        }   

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCatByUserId(int userId)
        {
            var result = await _mediator.Send(new GetCatByUserIdQuery(userId));
            return Ok(result);
        }
    }
}   
