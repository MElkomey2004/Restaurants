

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;

namespace Restaurants.Application.Users.Commands
{
	public class UpdateUserDetalisCommandHandler(ILogger<UpdateUserDetalisCommandHandler> logger , 
		IUserContext userContext , IUserStore<User> userStore) : IRequestHandler<UpdateUserDetalisCommand>
	{
		public async Task Handle(UpdateUserDetalisCommand request, CancellationToken cancellationToken)
		{
			var user = userContext.GetCurrentUser();

			logger.LogInformation("Updating user: {UserId}, with {@Request}", user!.Id, request);

			var dbUser = await userStore.FindByIdAsync(user!.Id, cancellationToken);

			if (dbUser == null)
			{
				throw new NotfoundException(nameof(User), user!.Id);
			}

			dbUser.Nationality = request.Nationality;
			dbUser.DateOfBirth = request.DateOfBirth;

			await userStore.UpdateAsync(dbUser, cancellationToken);
		}
	}
}
