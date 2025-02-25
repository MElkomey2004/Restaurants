

using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants;

internal class RestaurantsService : IRestaurantsService
{
	private readonly IRestaurantsRepository _restaurantsRepository;
	private readonly ILogger<RestaurantsService> _looger;
	private readonly IMapper _mapper;
	public RestaurantsService(IRestaurantsRepository restaurantsRepository, ILogger<RestaurantsService> looger,
		IMapper mapper )
	{
		_restaurantsRepository = restaurantsRepository;
		_looger = looger;
		_mapper = mapper; 

	}
	public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
	{
		_looger.LogInformation("Getting all restaruants");
		var restaurants = await _restaurantsRepository.GetAllAsync();

		var restauranstDto = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants); 
			
		return restauranstDto!;

	}

	public async  Task<RestaurantDto?> GetById(int id)
	{
		_looger.LogInformation("Getting restaurant By id");

		var restaurant = await _restaurantsRepository.GetByIdAsync(id);
		var restauranstDto = _mapper.Map<RestaurantDto>(restaurant);
		return restauranstDto;

		
	}

	public async Task<int>  Create(CreateRestaurantDto dto)
	{
		_looger.LogInformation("Creating a new restaurant");

		var restaurant = _mapper.Map<Restaurant>(dto);

		int id = await _restaurantsRepository.Create(restaurant);
		return id;
	}


}
