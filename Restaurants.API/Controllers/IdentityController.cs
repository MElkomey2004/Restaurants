using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands;

namespace Restaurants.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class IdentityController(IMediator mediator) : ControllerBase
	{
		[HttpPatch("user")]
		[Authorize]
		
		public async Task<IActionResult> UpdateUserDetalis(UpdateUserDetalisCommand command)
		{
			await mediator.Send(command);
			return NoContent();

		}
	}
}
