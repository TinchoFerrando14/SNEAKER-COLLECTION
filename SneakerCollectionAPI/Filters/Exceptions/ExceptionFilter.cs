using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SneakerCollectionAPI.Contracts;
using SneakerCollectionAPI.Exceptions;
using System.Net;

namespace SneakerCollectionAPI.Filters.Exceptions
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = HttpStatusCode.InternalServerError;
            var error = new Error
            {
                StatusCode = "500",
                Message = "Internal Server Error" + context.Exception.Message
            };
           

            if (context.Exception.GetType() == typeof(BadRequestException))
            {              
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                return;
            }
            else if (context.Exception.GetType() == typeof(SneakerOwnershipException))
            {
                context.Result = new ForbidResult();
                return;
            }

            context.Result = new JsonResult(error) { StatusCode = (int) statusCode };
        }
    }
}
