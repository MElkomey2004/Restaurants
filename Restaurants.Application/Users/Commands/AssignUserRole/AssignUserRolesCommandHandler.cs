

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands.AssignUserRole
{
	public class AssignUserRolesCommandHandler(ILogger<AssignUserRolesCommandHandler> logger,
	UserManager<User> userManager,
	RoleManager<IdentityRole> roleManager) : IRequestHandler<AssignUserRolesCommand>
	{
		public async Task Handle(AssignUserRolesCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Assigning user role: {@Request}", request);
			var user = await userManager.FindByEmailAsync(request.UserEmail)
				?? throw new NotfoundException(nameof(User), request.UserEmail);

			var role = await roleManager.FindByNameAsync(request.RoleName)
				?? throw new NotfoundException(nameof(IdentityRole), request.RoleName);

			await userManager.AddToRoleAsync(user, role.Name!);
		}
	}
}
