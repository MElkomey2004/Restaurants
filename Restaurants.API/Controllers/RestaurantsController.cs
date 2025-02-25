using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dtos;
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


    [HttpPost]

    public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto createRestaurantDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int id = await _restaurantsService.Create(createRestaurantDto);

        return CreatedAtAction(nameof(GetById),new {id} , null);
    
    }

}
