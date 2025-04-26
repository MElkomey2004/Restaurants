using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
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

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
	public class UpdateRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> looger, IMapper mapper,
		IRestaurantsRepository restaurantsRepository,
		IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<UpdateRestaurantCommand>
	{
		public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
		{
			looger.LogInformation("Update restaurant with id : {RestaurantId} with {@UpdateRestaurant}" , request.Id , request);
			var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

			if(restaurant == null )
			{
				throw new NotfoundException(nameof(Restaurant), request.Id.ToString());

			}
			if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperation.Update))
				throw new ForbidException();

			mapper.Map(request , restaurant);


			await restaurantsRepository.SaveChanges();

		
		}
	}
}
