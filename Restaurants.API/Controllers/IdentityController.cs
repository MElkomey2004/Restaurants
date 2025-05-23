﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Users.Commands.AssignUserRole;
using Restaurants.Application.Users.Commands.UnAssignUserRole;
using Restaurants.Application.Users.Commands.UpdateUserDetails;
using Restaurants.Domain.Constants;

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


		[HttpPost("userRole")]
		[Authorize(Roles =UserRoles.Admin)]
		public async Task<IActionResult> AssignUserRole(AssignUserRolesCommand command)
		{
			await mediator.Send(command);
			return NoContent();
		}
		[HttpDelete("userRole")]
		[Authorize(Roles = UserRoles.Admin)]
		public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
		{
			await mediator.Send(command);
			return NoContent();
		}

	}
}
