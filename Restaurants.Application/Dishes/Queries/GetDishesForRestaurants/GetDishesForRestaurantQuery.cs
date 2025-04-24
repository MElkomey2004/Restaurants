using MediatR;
using Restaurants.Application.Dishes.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurants
{
	public class GetDishesForRestaurantQuery(int RestaurantId) : IRequest<IEnumerable<DishDto>>
	{
		public int RestaurantId { get; } = RestaurantId;
	}
}
