using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDishe;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace Restaurants.API.Controllers
{
	[Route("api/restaurants/{restaurantId}/dishes")]
	[ApiController]
	[Authorize]
	public class DishesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DishesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> CreateDish(
			[FromRoute] int restaurantId,
			[FromBody] CreatedDishCommand command)
		{
			var dishId = command.RestaurantId = restaurantId;

			await _mediator.Send(command);
			return CreatedAtAction(nameof(GetDishByIdForRestaurant) ,new { restaurantId , dishId} ,null);
		}

		[HttpGet]
		[Authorize(Policy = PolicyNames.AtLeast20)]
		public async Task<ActionResult<IEnumerable<DishDto>>> GetAllDishesForRestaurant(
			[FromRoute] int restaurantId)
		{
			var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
			return Ok(dishes);
		}

		[HttpGet("{dishId}")]
		public async Task<ActionResult<DishDto>> GetDishByIdForRestaurant(
			[FromRoute] int restaurantId,
			[FromRoute] int dishId)
		{
			var dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));

			if (dish == null)
				return NotFound();

			return Ok(dish);
		}

		[HttpDelete]
		public async Task<IActionResult> RemoveAlldishesForSpecificRestaurant([FromRoute] int restaurantId)
		{
			 await _mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
			return NoContent();

;
		}
	}
}
