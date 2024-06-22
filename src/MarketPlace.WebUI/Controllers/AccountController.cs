
using MarketPlace.Application.App.Login.AuthModels;
using MarketPlace.Application.App.Login.Commands;
using MarketPlace.Application.App.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace MarketPlace.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LogInCommand signInCommand)
        {
            var response = await _mediator.Send(signInCommand);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel RegisterData)
        {
            var response = await _mediator.Send(new RegisterCommand(RegisterData));
            return Ok(response);
        }

        
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetUserById(int id) 
        {
            var response = await _mediator.Send(new GetUserByIdQuery(id));
            return Ok(response);
        }

        [Authorize]
        [HttpGet("user")]
        public async Task<IActionResult> GetUserFromToken()
        {
            var respnse = await _mediator.Send(new GetUserFromTokenQuery());    
            return Ok(respnse);
        }
    }
}
