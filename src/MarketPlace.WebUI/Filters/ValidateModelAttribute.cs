using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MarketPlace.WebUI.Middlewares.Models;

namespace MarketPlace.WebUI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var apiError = new ErrorDetail
                {
                    StatusCode = 400,
                    Message = "Bad Request",
                    Time = DateTime.UtcNow
                 
                };

                foreach (var error in context.ModelState)
                {
                    foreach (var inner in error.Value.Errors)
                    {
                        apiError.Errors.Add(inner.ErrorMessage);
                    }
                }

                context.Result = new BadRequestObjectResult(apiError);
            }
        }
    }
}
