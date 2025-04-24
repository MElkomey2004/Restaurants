using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Commands.CreateDishe;

public class CreateDishCommandValidator : AbstractValidator<CreatedDishCommand>
{
    public CreateDishCommandValidator()
    {
        RuleFor(dish => dish.Price)
            .GreaterThanOrEqualTo(0).WithMessage("price must be a non negative number");

		RuleFor(dish => dish.KiloCalories)
		  .GreaterThanOrEqualTo(0).WithMessage("Kalories must be a non negative number");
	}
}
