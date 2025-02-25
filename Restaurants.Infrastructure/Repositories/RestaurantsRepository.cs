
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories
{
	internal class RestaurantsRepository : IRestaurantsRepository
	{
		private readonly RestaurantsDbContext _db;
		public RestaurantsRepository(RestaurantsDbContext db)
		{
			_db  = db;
		}

		public async Task<IEnumerable<Restaurant>> GetAllAsync()
		{
			var restaurants = await _db.Restaurants.ToListAsync();

			return restaurants;
		}

		public async Task<Restaurant?> GetByIdAsync(int id)
		{
			var restaurant = await _db.Restaurants
				.Include(r => r.Dishes).FirstOrDefaultAsync(x => x.Id == id);	
		
			return restaurant;
		}
	}
}
