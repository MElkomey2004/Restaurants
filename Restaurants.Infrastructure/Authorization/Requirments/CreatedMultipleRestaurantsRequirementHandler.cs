using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;


namespace Restaurants.Infrastructure.Authorization.Requirments;

public class CreatedMultipleRestaurantsRequirementHandler(IRestaurantsRepository restaurantsRepository,
	IUserContext userContext) : AuthorizationHandler<CreateMulipleRestaurantsRequirment>
{
	protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
		CreateMulipleRestaurantsRequirment requirement)
	{
		var currentUser = userContext.GetCurrentUser();

		var restaurants = await restaurantsRepository.GetAllAsync();

		var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser!.Id);

		if (userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
		{
			context.Succeed(requirement);
		}
		else
		{
			context.Fail();
		}
	}
}
