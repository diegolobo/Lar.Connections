using FluentValidation;

namespace Lar.Connections.Application.UseCases.People.DeletePersonPhone;

public class DeletePersonPhoneValidator : AbstractValidator<DeletePersonPhoneCommand>
{
	public DeletePersonPhoneValidator()
	{
		RuleFor(x => x.PersonId)
			.NotEmpty()
			.NotEqual(0)
			.WithMessage("PersonId is required.");

		RuleFor(x => x.Id)
			.NotEmpty()
			.NotEqual(0)
			.WithMessage("Id is required.");
	}
}