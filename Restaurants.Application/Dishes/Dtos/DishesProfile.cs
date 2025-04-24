using AutoMapper;
using Restaurants.Application.Dishes.Commands.CreateDishe;
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dtos
{
	public class DishesProfile : Profile
	{
		public DishesProfile()
		{
			CreateMap<Dish, DishDto>().ReverseMap();
			CreateMap<CreatedDishCommand, Dish>().ReverseMap();
		}
	}
}