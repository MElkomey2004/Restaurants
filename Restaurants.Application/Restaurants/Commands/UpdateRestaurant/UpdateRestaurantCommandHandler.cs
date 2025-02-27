using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
	public class UpdateRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> looger, IMapper mapper,
		IRestaurantsRepository restaurantsRepository) : IRequestHandler<UpdateRestaurantCommand, bool>
	{
		public async Task<bool> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
		{
			looger.LogInformation($"Update restaurant with id : {request.Id}");
			var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

			if(restaurant == null )
			{
				return false;
			}

			mapper.Map(request , restaurant);


			await restaurantsRepository.SaveChanges();

			return true;
		}
	}
}
