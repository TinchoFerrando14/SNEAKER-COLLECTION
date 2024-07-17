using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SneakerCollectionAPI.DataAccess;
using MediatR;
using SneakerCollectionAPI.DataAccess.SneakerDataAccess.Queries;
using SneakerCollectionAPI.Exceptions;
using SneakerCollectionAPI.Domain.Enums;

namespace SneakerCollectionAPI.Filters.Validators
{
    public class SneakerOwnershipFilter : IAsyncActionFilter
    {
        private readonly IMediator _mediator;

        public SneakerOwnershipFilter(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ActionArguments.TryGetValue("id", out var value) && value is long sneakerId)
            {
                var userId = context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                var userRole = context.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

                var sneaker = await _mediator.Send(new GetSneakerByIdQuery(sneakerId));

                if (sneaker == null)
                {
                    context.Result = new NotFoundResult();
                    return;
                }

                if (userRole == RoleEnum.Collector.ToString())
                {
                    if (sneaker.UserId.ToString() != userId)
                    {
                        throw new SneakerOwnershipException();
                    }
                }
              
            }
            else
            {
                context.Result = new BadRequestResult();
                return;
            }

            await next();
        }
    }
}
