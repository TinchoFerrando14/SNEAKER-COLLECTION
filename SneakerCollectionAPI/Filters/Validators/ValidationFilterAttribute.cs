using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SneakerCollectionAPI.Contracts;
using SneakerCollectionAPI.Exceptions;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace SneakerCollectionAPI.Middlewares.Validators
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                throw new BadRequestException();
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { }
      
    }
}
