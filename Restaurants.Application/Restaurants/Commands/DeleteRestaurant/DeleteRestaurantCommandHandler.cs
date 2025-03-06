using AutoMapper;
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

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
	public class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> looger, IMapper mapper , 
		IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
	{
		public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
		{
			looger.LogInformation("Deleting restaurant with id : {RestaurantId}", request.Id);
			var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

			if (restaurant is null)
				throw  new NotfoundException(nameof(Restaurant),request.Id.ToString());

			await restaurantsRepository.Delete(restaurant);

	
		}
	}
}
