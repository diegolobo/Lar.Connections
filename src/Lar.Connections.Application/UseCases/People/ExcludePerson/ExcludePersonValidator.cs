using FluentValidation;

namespace Lar.Connections.Application.UseCases.People.ExcludePerson;

public class ExcludePersonValidator : AbstractValidator<ExcludePersonCommand>
{
	public ExcludePersonValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.WithMessage("Id is required.");
	}
}