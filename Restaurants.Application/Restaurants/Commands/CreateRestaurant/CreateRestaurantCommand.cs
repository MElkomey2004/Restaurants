

using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

    public class CreateRestaurantCommand : IRequest<int>
    {

	[StringLength(100, MinimumLength = 3)]
	public string Name { get; set; } = default!;
	public string Description { get; set; } = default!;

	[Required(ErrorMessage = "Insert a valid Category")]
	public string Categroy { get; set; } = default!;

	[Required(ErrorMessage = "Insert a valid Category")]
	public bool HasDelivery { get; set; }

	[EmailAddress(ErrorMessage = "Please provide a valid email address")]

	public string? ContactEmail { get; set; }
	[Phone(ErrorMessage = "Pleaser provide a valid phone number")]
	public string? ContactNumber { get; set; }

	public string? City { get; set; }
	public string? Street { get; set; }

	[RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Pleaser provide a valid postal code (XX-XXX).")]
	public string? PostalCode { get; set; }
}
