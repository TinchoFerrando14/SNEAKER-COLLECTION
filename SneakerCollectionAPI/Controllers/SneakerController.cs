using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.CreateSneaker;
using SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.DeleteSneaker;
using SneakerCollectionAPI.DataAccess.SneakerDataAccess.Commands.UpdateSneaker;
using SneakerCollectionAPI.DataAccess.SneakerDataAccess.Queries;
using SneakerCollectionAPI.Domain.DTOs;
using SneakerCollectionAPI.Filters.Validators;
using SneakerCollectionAPI.Middlewares.Validators;
using System.Security.Claims;

namespace SneakerCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SneakerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SneakerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Sneaker([FromBody] NewSneakerInputDto sneakerDto) 
        {     
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var command = new CreateSneakerCommand
            {
                Name = sneakerDto.Name,
                Brand = sneakerDto.Brand,
                Price = sneakerDto.Price,
                Size = sneakerDto.Size,
                Year = sneakerDto.Year,
                Rate = sneakerDto.Rate,
                UserId = Convert.ToInt64(userId)
            };

            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetSneakerById), new { id = result.Id }, result);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ServiceFilter(typeof(SneakerOwnershipFilter))]

        public async Task<IActionResult> DeleteSneaker(long id)
        {
            var result = await _mediator.Send(new DeleteSneakerCommand { Id = id });

            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var sneakers = await _mediator.Send(new GetSneakerByUserQuery(Convert.ToInt64(userId), pageNumber, pageSize));
            return Ok(sneakers);
        }

        [HttpGet("{id}")]
        [Authorize]
        [ServiceFilter(typeof(SneakerOwnershipFilter))]

        public async Task<IActionResult> GetSneakerById(long id)
        {
            var sneaker = await _mediator.Send(new GetSneakerByIdQuery(id));
            if (sneaker == null)
            {
                return NotFound();
            }

            return Ok(sneaker);
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(SneakerOwnershipFilter))]
        public async Task<IActionResult> UpdateSneaker(long id, [FromBody] UpdateSneakerInputDto sneaker)
        {
            if (id != sneaker.Id)
            {
                return BadRequest("ID mismatch");
            }

            var updateSneakerCommand = new UpdateSneakerCommand()
            {
                Id = id,
                Name = sneaker.Name,
                Brand = sneaker.Brand,
                Size = sneaker.Size,
                Price = sneaker.Price,
                Rate = sneaker.Rate,
                Year = sneaker.Year
            };

            var updatedSneaker = await _mediator.Send(updateSneakerCommand);
            if (updatedSneaker == null)
            {
                return NotFound();
            }

            return Ok(updatedSneaker);
        }
    }
}
