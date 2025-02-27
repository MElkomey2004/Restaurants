
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
	public class UpdateRestaurantCommand  :IRequest<bool>
	{
		public int Id { get; set; }

		[StringLength(100, MinimumLength = 3)]
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;

		[Required(ErrorMessage = "Insert a valid Category")]
		public bool HasDelivery { get; set; }

	
	}
}
