

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.UnAssignUserRole;

public class UnassignUserRoleCommandHandler(ILogger<UnassignUserRoleCommandHandler> logger, UserManager<User> userManager
	, RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
{
	public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
	{
		logger.LogInformation("Unassigning user role: {@Request}", request);
		var user = await userManager.FindByEmailAsync(request.UserEmail)
			?? throw new NotfoundException(nameof(User), request.UserEmail);

		var role = await roleManager.FindByNameAsync(request.RoleName)
			?? throw new NotfoundException(nameof(IdentityRole), request.RoleName);

		await userManager.RemoveFromRoleAsync(user, role.Name!);
	}
}
