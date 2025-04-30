using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.Authorization.Requirments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Restaurants.Infrastructure.Authorization.Requirments.Tests
{
	[TestClass()]
	public class CreatedMultipleRestaurantsRequirementHandlerTests
	{
		[Fact()]
		public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
		{
			// arrange

			var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
			var userContextMock = new Mock<IUserContext>();
			userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

			var restaurants = new List<Restaurant>()
			{
				new()
				{
					OwnerId = currentUser.Id,
				},
				new()
				{
					OwnerId = "2",
				},
			};

			var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
			restaurantsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

			var requirement = new CreateMulipleRestaurantsRequirment(2);
			var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantsRepositoryMock.Object,
				userContextMock.Object);
			var context = new AuthorizationHandlerContext([requirement], null, null);

			// act

			await handler.HandleAsync(context);

			// assert

			context.HasSucceeded.Should().BeFalse();
			context.HasFailed.Should().BeTrue();

		}

		[Fact()]
		public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSucceed()
		{
			// arrange

			var currentUser = new CurrentUser("1", "test@test.com", [], null, null);
			var userContextMock = new Mock<IUserContext>();
			userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

			var restaurants = new List<Restaurant>()
			{
				new()
				{
					OwnerId = currentUser.Id,
				},
				new()
				{
					OwnerId = currentUser.Id,
				},
				new()
				{
					OwnerId = "2",
				},
			};

			var restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
			restaurantsRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(restaurants);

			var requirement = new CreateMulipleRestaurantsRequirment(2);
			var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantsRepositoryMock.Object,
				userContextMock.Object);
			var context = new AuthorizationHandlerContext([requirement], null, null);

			// act

			await handler.HandleAsync(context);

			// assert

			context.HasSucceeded.Should().BeTrue();

		}
	}
}