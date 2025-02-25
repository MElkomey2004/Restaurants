

using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants;

internal class RestaurantsService : IRestaurantsService
{
	private readonly IRestaurantsRepository _restaurantsRepository;
	private readonly ILogger<RestaurantsService> _looger;
	public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> looger)
	{
		_restaurantsRepository = restaurantsRepository;
		_looger = looger;

	}
	public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
	{
		_looger.LogInformation("Getting all restaruants");
		var restaurants = await _restaurantsRepository.GetAllAsync();

		var restauranstDto = restaurants.Select(RestaurantDto.FromEntity);
			
		return restauranstDto!;

	}

	public async  Task<RestaurantDto?> GetById(int id)
	{
		_looger.LogInformation("Getting restaurant By id");

		var restaurant = await _restaurantsRepository.GetByIdAsync(id);
		var restauranstDto = RestaurantDto.FromEntity(restaurant);
		return restauranstDto;

		
	}



}
