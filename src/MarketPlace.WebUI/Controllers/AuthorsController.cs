using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Querries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MarketPlace.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthorsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthor command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAuthor(UpdateAuthor command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound($"No such Author");
            }
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuerry());
            if(authors.IsNullOrEmpty())
            {
                return NotFound("There are no authors");
            }
            return Ok(authors);
        }

        [HttpGet("country")]
        public async Task<IActionResult> GetAuthorsByCountry(string country)
        {
            var authors = await _mediator.Send(new GetAllAuthorsByCountryQuerry(country));
            if(authors.IsNullOrEmpty())
            {
                return NotFound($"No authors found for such country : '{country}' ");
            }
            return Ok(authors);              
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _mediator.Send(new DeleteAuthor(id));

            if(author == null)
            {
                return NotFound($"No such author with Id:{id}");
            }   
            return Ok(author);  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuerry(id));
            if(author == null)
            {
                return NotFound($"No authr with such Id:{id}");
            }

            return Ok(author);  
        }
    }
}
