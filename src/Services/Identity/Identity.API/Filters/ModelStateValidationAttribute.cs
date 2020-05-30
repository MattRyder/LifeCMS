using System.Linq;
using LifeCMS.Services.Identity.Infrastructure.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LifeCMS.Services.Identity.API.Filters
{
    public class ModelStateValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var response = GenerateResponse(context.ModelState);

            context.Result = new BadRequestObjectResult(response);
        }

        private BasicResponse GenerateResponse(ModelStateDictionary modelState)
        {
            var response = new BasicResponse();

            var errors = modelState
                .SelectMany(selector => selector.Value.Errors)
                .Select((err) => err.ErrorMessage);

            return new BasicResponse
            {
                Success = false,
                Errors = errors,
            };
        }
    }
}