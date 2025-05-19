using FluentValidation;

namespace Lar.Connections.Application.UseCases.People.UpdatePerson;

public class UpdatePersonValidator : AbstractValidator<UpdatePersonCommand>
{
	public UpdatePersonValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.WithMessage("Id is required.");

		RuleFor(x => x.Name)
			.NotEmpty()
			.WithMessage("Name is required.");

		RuleFor(x => x.Document)
			.NotEmpty()
			.WithMessage("Document is required.")
			.EmailAddress()
			.WithMessage("Document is not valid.");

		RuleFor(x => x.BirthDate)
			.NotEmpty()
			.NotEqual(DateTime.MinValue)
			.WithMessage("BirthDate number is required.");
	}
}