using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
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
	IDishesRepository dishesRepository) : IRequestHandler<DeleteDishesForRestaurantCommand>
	{
		public async Task Handle(DeleteDishesForRestaurantCommand request, CancellationToken cancellationToken)
		{

			logger.LogWarning("Removing all dishes from restaurant: {RestaurantId}", request.RestaurantId);

			var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId);
			if (restaurant == null) throw new NotfoundException(nameof(Restaurant), request.RestaurantId.ToString());

			await dishesRepository.Delete(restaurant.Dishes);

		}
	}
}
