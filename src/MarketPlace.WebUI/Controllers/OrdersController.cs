using MarketPlace.Application.App.Orders.Querries;
using MarketPlace.Application.Orders.Create;
using MarketPlace.Application.Orders.Delete;
using MarketPlace.Application.Orders.Update;
using MarketPlace.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


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
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> CreateOrder(CreateOrder command)
        {
            var orderResult = await _mediator.Send(command);
            return Ok(orderResult);
        }


        [HttpPut]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> UpdateOrder(UpdateOrder command)
        {
            var result = await _mediator.Send(command);          
            return Ok(result);  
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orderResult = await _mediator.Send(new DeleteOrder(id));
            return Ok(orderResult);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var orderResult = await _mediator.Send(new GetOrderByIdQuerry(id));         
            return Ok(orderResult);
        }
        
        
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _mediator.Send(new GetAllOrderQuerry());         
            return Ok(orders);
        }

    }
}
