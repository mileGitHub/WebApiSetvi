using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApiSetvi.Model.Validation
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                string errorMessages = string.Join("; ", context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                context.Result = new BadRequestObjectResult(new Result<object>(HttpStatusCode.BadRequest, errorMessages));
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
