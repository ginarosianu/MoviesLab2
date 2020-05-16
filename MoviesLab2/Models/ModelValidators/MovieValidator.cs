using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesLab2.Models.ModelValidators
{
    public class MovieValidator : AbstractValidator<Movie>
	{
		public MovieValidator()
		{
			RuleFor(t => t.Title).MinimumLength(2);
            RuleFor(r => r.Rating).InclusiveBetween (1,10);
			RuleFor(d => d.Description).MaximumLength(25);
			RuleFor(y => y.YearOfRelease).InclusiveBetween(1950, 2020);
			RuleFor(d => d.DateAdded).LessThan(DateTime.Now).WithMessage("DateAdded must be lower that curent date");
		}
	}
}
