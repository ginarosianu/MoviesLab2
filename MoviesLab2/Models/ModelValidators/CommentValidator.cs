using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesLab2.Models.ModelValidators
{
	public class CommentValidator : AbstractValidator<Comment>
	{
		public CommentValidator()
		{
			RuleFor(c => c.Content).MaximumLength(20).WithMessage("The content of the comment must be no longer than 20");
			RuleFor(a => a.Author).MinimumLength(2);
			
		}
	}
}
