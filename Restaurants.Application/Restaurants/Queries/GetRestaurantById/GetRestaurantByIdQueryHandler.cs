using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
	public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> looger , IMapper mapper , IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
	{
		public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
		{
			looger.LogInformation("Getting restaurant By id");

			var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);
			var restauranstDto = mapper.Map<RestaurantDto>(restaurant);
			return restauranstDto;

		}
	}
}
