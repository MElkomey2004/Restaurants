
using MediatR;

namespace Restaurants.Application.Users.Commands
{
	public class UpdateUserDetalisCommand :IRequest
	{
		public DateOnly? DateOfBirth { get; set; }
		public string? Nationality { get; set; }
	}
}
