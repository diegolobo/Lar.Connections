using FluentValidation;

namespace Lar.Connections.Application.UseCases.People.UpdatePersonPhone;

public class UpdatePersonPhoneValidator : AbstractValidator<UpdatePersonPhoneCommand>
{
	public UpdatePersonPhoneValidator()
	{
		RuleFor(x => x.Id)
			.NotEmpty()
			.NotEqual(0)
			.WithMessage("Id is required.");
	}
}