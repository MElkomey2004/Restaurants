using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Domain.Entities;

namespace Restaurants.API.Controllers;


[ApiController]
[Route("api/restaurants")]
public class RestaurantsController: ControllerBase
{
    private readonly IRestaurantsService _restaurantsService;


	public RestaurantsController(IRestaurantsService restaurantsService)
    {
        _restaurantsService = restaurantsService;   
        
    }

    [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var results = await _restaurantsService.GetAllRestaurants();

        return Ok(results);
    }

    [HttpGet]
    [Route("{id:int}")]

	public async Task<IActionResult> GetById([FromRoute]int id)
	{
        var result  = await _restaurantsService.GetById(id);
        
        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
	}

}
