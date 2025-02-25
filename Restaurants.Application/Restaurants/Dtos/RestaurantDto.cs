

using Restaurants.Application.Dishes.Dtos;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Dtos;

public class RestaurantDto
{

	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;
	public string Categroy { get; set; } = default!;
	public bool HasDelivery { get; set; }
	public string? City { get; set; }
	public string? Street { get; set; }
	public string? PostalCode { get; set; }

	public List<DishDto> Dishes { get; set; } = [];


	public static RestaurantDto? FromEntity(Restaurant? restaurant)
	{
		if (restaurant == null)
			return null;
		return new RestaurantDto()
		{

			Categroy = restaurant.Categroy,
			Description = restaurant.Description,
			Id = restaurant.Id,
			HasDelivery = restaurant.HasDelivery,
			Name = restaurant.Name,
			City = restaurant.Address?.City,
			Street = restaurant.Address?.Street,
			PostalCode = restaurant.Address?.PostalCode,
			Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()


		};
	}


}
