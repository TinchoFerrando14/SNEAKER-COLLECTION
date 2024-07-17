using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerCollectionAPI.DataAccess.UserDataAccess.Commands.CreateUser;
using SneakerCollectionAPI.DataAccess.UserDataAccess.Queries.GetUsers;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Domain.Entities;
using SneakerCollectionAPI.Middlewares.Validators;

namespace SneakerCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> User([FromBody] NewUserInputDto user)
        {
            //validaciones

            var command = new CreateUserCommand() { Email = user.Email, Password = user.Password, Role = "Collector" };

            var result = await _mediator.Send(command);

            if (result == null)
            {
                return Conflict("User with this email already exists.");
            }

            return Ok("User registered successfully.");
        }

        [HttpGet]
        [Authorize(Roles = "BackOffice")]
        public async Task<IActionResult> Get()
        {
            var users = await _mediator.Send(new GetUsersQuery());
            return Ok(users);
        }
    }
}
