using MarketPlace.Application.App.Authors.Commands;
using MarketPlace.Application.App.Authors.Querries;
using MarketPlace.Application.App.Categories.Querries;
using MarketPlace.WebUI.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [ValidateModel]
        public async Task<IActionResult> CreateAuthor([FromForm] CreateAuthor command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut]
        [ValidateModel]
        [Authorize(Roles = "Author")]
        public async Task<IActionResult> UpdateAuthor([FromForm] UpdateAuthor command)
        {
            var result = await _mediator.Send(command);
            if (result == null)
            {
                return NotFound($"No such Author");
            }
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _mediator.Send(new GetAllAuthorsQuerry());
            if(authors.IsNullOrEmpty())
            {
                return NotFound("There are no authors");
            }
            return Ok(authors);
        }

        [HttpGet("authorsBycountry")]
        [ValidateModel]
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
            return Ok(author);  
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _mediator.Send(new GetAuthorByIdQuerry(id));
            return Ok(author);  
        }

        [HttpGet("countries")]
        public async Task<IActionResult> GetAllCountries()
        {
            var countries = await _mediator.Send(new GetAllCountriesQuery());
            return Ok(countries);
        }

        [HttpGet("names")]
        public async Task<IActionResult> GetAllAuthorNames()
        {
            var countries = await _mediator.Send(new GetAllAuthorsNameQuery());
            return Ok(countries);
        }


        [HttpGet("by-user/{id}")]
        public async Task<IActionResult> GetAuthorByUserId(int id)
        {
            var author = await _mediator.Send(new GeAuthorByUserIdQuerry(id));
            if (author == null)
            {
                return NotFound($"No authr with such Id:{id}");
            }

            return Ok(author);
        }


    }
}
