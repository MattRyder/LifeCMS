using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Socialite.Authentication.Application.Responses;

namespace Socialite.Authentication.Filters
{
    public class ModelStateValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
            {
                return;
            }

            var response = GenerateCommandResponse(context.ModelState);

            context.Result = new BadRequestObjectResult(response);
        }

        private CommandResponse GenerateCommandResponse(ModelStateDictionary modelState)
        {
            var response = new CommandResponse();

            var errors = modelState
                .SelectMany(selector => selector.Value.Errors)
                .Select((err) => err.ErrorMessage);

            return new CommandResponse {
                Success = false,
                Errors = errors,
            };
        }
    }
}