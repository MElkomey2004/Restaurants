using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;


[ApiController]
[Route("api/restaurants")]
public class RestaurantsController: ControllerBase
{
    private readonly IMediator _mediatior;


	public RestaurantsController(IMediator mediator)
    {
        _mediatior = mediator;   
        
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var results = await _mediatior.Send(new GetAllRestaurantsQuery());

        return Ok(results);
    }

    [HttpGet]
    [Route("{id:int}")]

	public async Task<IActionResult> GetById([FromRoute]int id)
	{
        var result = await _mediatior.Send(new GetRestaurantByIdQuery(id));
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

	[HttpDelete("id")]

	public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
	{
		var isDeleted = await _mediatior.Send(new DeleteRestaurantCommand(id));

		if (isDeleted)
			return NoContent();
	

		return NotFound();
	}



	[HttpPatch("id")]

	public async Task<IActionResult> UpdateRestaurant([FromRoute] int id , UpdateRestaurantCommand command)
	{
        command.Id = id;
		var isUpdated = await _mediatior.Send(command);


		if (isUpdated)
			return NoContent();


		return NotFound();
	}

	[HttpPost]

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
