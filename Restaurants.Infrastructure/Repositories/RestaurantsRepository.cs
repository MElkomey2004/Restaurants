﻿
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Persistence;
using System.Linq.Expressions;

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
		public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchPhrase,
	   int pageSize,
	   int pageNumber,
	   string? sortBy,
	   SortDirection sortDirection)
		{
			var searchPhraseLower = searchPhrase?.ToLower();

			var baseQuery = _db
				.Restaurants
				.Where(r => searchPhraseLower == null || (r.Name.ToLower().Contains(searchPhraseLower)
													   || r.Description.ToLower().Contains(searchPhraseLower)));

			var totalCount = await baseQuery.CountAsync();

			if (sortBy != null)
			{
				var columnsSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>
			{
				{ nameof(Restaurant.Name), r => r.Name },
				{ nameof(Restaurant.Description), r => r.Description },
				{ nameof(Restaurant.Categroy), r => r.Categroy },
			};

				var selectedColumn = columnsSelector[sortBy];

				baseQuery = sortDirection == SortDirection.Ascending
					? baseQuery.OrderBy(selectedColumn)
					: baseQuery.OrderByDescending(selectedColumn);
			}

			var restaurants = await baseQuery
				.Skip(pageSize * (pageNumber - 1))
				.Take(pageSize)
				.ToListAsync();

			return (restaurants, totalCount);
		}


		public async Task<Restaurant?> GetByIdAsync(int id)
		{
			var restaurant = await _db.Restaurants
				.Include(r => r.Dishes).FirstOrDefaultAsync(x => x.Id == id);	
		
			return restaurant;
		}

		public async Task<int> Create(Restaurant entity)
		{
			_db.Restaurants.Add(entity);
			await _db.SaveChangesAsync();
			return entity.Id;

		}

		public async Task Delete(Restaurant entity)
		{
			_db.Remove(entity);
			await _db.SaveChangesAsync();

		}

		
		public async Task SaveChanges()
		{
				await _db.SaveChangesAsync();	
		}
	}
}
