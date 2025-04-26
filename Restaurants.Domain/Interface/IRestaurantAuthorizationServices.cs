using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Interface
{
	public interface IRestaurantAuthorizationServices
	{
		bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation);
	}
}