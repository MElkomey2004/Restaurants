using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interface;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.DeleteDishes
{
	public class DeleteDishesForRestaurantCommandHandler(ILogger<DeleteDishesForRestaurantCommandHandler> logger,
	IRestaurantsRepository restaurantsRepository,
	IDishesRepository dishesRepository,
	IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<DeleteDishesForRestaurantCommand>
	{
		public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
		{

			logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);

			var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
			if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.RestaurantId.ToString());

			if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperation.Delete))
				throw new ForbidException();

			await dishesRepository.Delete(restaurant.Dishes);

		}
	}
}
