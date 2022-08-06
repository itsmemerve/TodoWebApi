using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace BarclaysToDos.Services.Core
{
    public class ValidationFilter : IAsyncActionFilter
    {
        /// <summary>
        /// a coding has been made that will return an error if there is no ModelState.Isvalid. 
        /// First of all, we caught the errors coming from ModelState. 
        /// The errors we caught were returned with foreach and added on the instance we created for Error management.
        /// </summary>
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key,
                                  kvp => kvp.Value.Errors.Select(x => x.ErrorMessage))
                    .ToArray();

                ErrorResponse errorReponse = new ErrorResponse();

                foreach (var error in errorsInModelState)
                {
                    foreach (var subError in error.Value)
                    {
                        ErrorModel errorModel = new ErrorModel
                        {
                            Status = (int)HttpStatusCode.BadRequest,
                            Field = error.Key,
                            Message = subError
                        };

                        errorReponse.Errors.Add(errorModel);
                    }
                }

                context.Result = new BadRequestObjectResult(errorReponse);
                return;
            }

            await next();
        }
    }
}
