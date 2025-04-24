using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories;

public class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
	public async Task<int> Create(Dish entity)
	{
		 dbContext.Dishes.Add(entity);
		await dbContext.SaveChangesAsync();
		return entity.Id;
	}

	public async Task Delete(IEnumerable<Dish> entities)
	{
		 dbContext.Dishes.RemoveRange(entities);
	     await dbContext.SaveChangesAsync();
	}
}
