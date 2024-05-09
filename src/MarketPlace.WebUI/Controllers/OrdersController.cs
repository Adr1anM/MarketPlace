using MarketPlace.Application.App.Orders.Querries;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Application.Orders.Update;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;           
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrder command)
        {
            var orderResult = await _mediator.Send(command);

            if(orderResult == null)
            {
                return BadRequest($"Unable to create this order ");
            }
            return Ok(orderResult);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(UpdateOrder command)
        {
            var result = await _mediator.Send(command);
            if(result == null)
            {
                return NotFound($"Order with Id:{command.Id} not found");
            }
            return Ok(result);  
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderResult = await _mediator.Send(new DeleteOrder(id));
            return Ok(orderResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var orderResult = await _mediator.Send(new GetOrderByIdQuerry(id));
            if(orderResult == null)
            {
                return NotFound($"Order with such Id:{id} not found");
            }
            return Ok(orderResult);
        }
            
        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetAllOrderQuerry());
            if (orders.IsNullOrEmpty())
            {
                return NotFound("There are no Orders");
            }
            return Ok(orders);
        }


    }
}
