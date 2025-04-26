using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers;


[ApiController]
[Route("api/restaurants")]
[Authorize]

public class RestaurantsController: ControllerBase
{
    private readonly IMediator _mediatior;


	public RestaurantsController(IMediator mediator)
    {
        _mediatior = mediator;   
        
    }

    [HttpGet]
    [Authorize(Policy =PolicyNames.CreatedAtLeast2Restaurants)]

    public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
    {
        var results = await _mediatior.Send(new GetAllRestaurantsQuery());

        return Ok(results);
    }

    [HttpGet]
    [Route("{id:int}")]
    [Authorize(Policy = PolicyNames.HasNationality)]
	public async Task<ActionResult<RestaurantDto>> GetById([FromRoute]int id)
	{
		var result = await _mediatior.Send(new GetRestaurantByIdQuery(id));
		return Ok(result);
    }

	[HttpDelete("id")]
	[ProducesResponseType(StatusCodes.Status204NoContent)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
	{
	
		await _mediatior.Send(new DeleteRestaurantCommand(id));
		return NoContent();
	

		
	}



	[HttpPatch("id")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
	public async Task<IActionResult> UpdateRestaurant([FromRoute] int id , UpdateRestaurantCommand command)
	{
        command.Id = id;
		await _mediatior.Send(command);
		return NoContent();
	}

	[HttpPost]
    [Authorize(Roles =UserRoles.Owner)]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int id = await _mediatior.Send(command);

        return CreatedAtAction(nameof(GetById),new {id} , null);
    
    }

}
