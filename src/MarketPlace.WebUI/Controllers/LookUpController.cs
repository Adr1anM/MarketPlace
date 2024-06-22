using MarketPlace.Application.App.Categories.Querries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookUpController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LookUpController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetAllProductCategories()
        {
            var categories = await _mediator.Send(new GetAllCategWithSubcateg());
            return Ok(categories);
        }

        [Authorize]
        [HttpGet("subcategories")]
        public async Task<IActionResult> GetAllProductSubCategories()
        {
            var categories = await _mediator.Send(new GetAllSubCategoriesQuerry());
            return Ok(categories);
        }

        [HttpGet("subcategories/{id}")]
        public async Task<IActionResult> GetAllProductSubCategories(int id)
        {
            var categories = await _mediator.Send(new GetSubCategoriesByCategIdQuerry(id));
            return Ok(categories);
        }



    }

}
