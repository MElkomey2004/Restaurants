using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interface;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDishe;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
	IRestaurantsRepository restaurantsRepository , IDishesRepository dishesRepository,
	IMapper mapper, 
	IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<CreatedDishCommand , int>
{
	public async Task<int> Handle(CreatedDishCommand request, CancellationToken cancellationToken)
	{
		try
		{
			logger.LogInformation("Creating new dish: {@DishRequest}", request);

			var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
			if (restaurant == null)
				throw new NotfoundException(nameof(Restaurant), request.RestaurantId.ToString());

			if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperation.Update))
				throw new ForbidException();

			var dish = mapper.Map<Dish>(request);
			dish.ResturantId = request.RestaurantId;

			return await dishesRepository.Create(dish);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, "Failed to create dish for restaurant {RestaurantId}", request.RestaurantId);
			throw;
		}
	}

}
