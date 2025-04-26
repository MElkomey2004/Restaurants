
using MediatR;

namespace Restaurants.Application.Users.Commands.UpdateUserDetails
{
    public class UpdateUserDetalisCommand : IRequest
    {
        public DateOnly? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
    }
}
