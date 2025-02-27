

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> _looger , IMapper _mapper , IRestaurantsRepository _restaurantsRepository) : IRequestHandler<CreateRestaurantCommand, int>
{
	public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
	{
		_looger.LogInformation("Creating a new restaurant");

		var restaurant = _mapper.Map<Restaurant>(request);

		int id = await _restaurantsRepository.Create(restaurant);
		return id;
	}
}
