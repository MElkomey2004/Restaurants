using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
	public interface IRestaurantsRepository
	{
		Task<IEnumerable<Restaurant>> GetAllAsync();
		Task<Restaurant?> GetByIdAsync(int id);

		Task<int> Create(Restaurant entity);

		Task Delete(Restaurant entity);

		Task SaveChanges();
		Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string SearchPhrase , int PageSize , int PageNumber , string? SortBy , SortDirection sortDirection);
	}
}




