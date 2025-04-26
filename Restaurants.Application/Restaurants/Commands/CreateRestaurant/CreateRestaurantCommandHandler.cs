

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandHandler(ILogger<CreateRestaurantCommandHandler> _looger , IMapper _mapper , IRestaurantsRepository _restaurantsRepository,
	IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
	public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
	{

		var currentUser = userContext.GetCurrentUser();
		_looger.LogInformation("{UserEmail} [{UserId}]Creating a new restaurant {@Restaurant}" ,currentUser.Email , currentUser.Id ,request);

		var restaurant = _mapper.Map<Restaurant>(request);
		restaurant.OwnerId = currentUser.Id;

		int id = await _restaurantsRepository.Create(restaurant);
		return id;
	}
}
