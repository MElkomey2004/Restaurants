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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
	public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> looger, IMapper mapper , 
		IRestaurantsRepository restaurantsRepository , IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<DeleteRestaurantCommand>
	{
		public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
		{
			looger.LogInformation("Deleting restaurant with id : {RestaurantId}", request.Id);
			var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

			if (restaurant is null)
				throw  new NotfoundException(nameof(Restaurant),request.Id.ToString());

			if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperation.Delete))
				throw new ForbidException();

			await restaurantsRepository.Delete(restaurant);

	
		}
	}
}
