using FluentValidation;

using static System.Text.RegularExpressions.Regex;

namespace Lar.Connections.Application.UseCases.People.NewPerson;

public class NewPersonValidator : AbstractValidator<NewPersonCommand>
{
	public NewPersonValidator()
	{
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

		When(x => x.Phones.Count > 0, () =>
		{
			RuleFor(x => x.Phones)
				.Must(phones => phones.Count <= 5)
				.WithMessage("Maximum of 5 phones allowed per person.");

			RuleForEach(x => x.Phones)
				.ChildRules(phone =>
				{
					phone.RuleFor(p => p.Number)
						.NotEmpty()
						.WithMessage("Phone number is required.")
						.Must(BeValidPhoneNumber)
						.WithMessage(
							"Phone number format is invalid. Expected format: (XX) XXXXX-XXXX or XX XXXXX-XXXX");

					phone.RuleFor(p => p.Type)
						.IsInEnum()
						.WithMessage("Invalid phone type.");
				});
		});
	}

	private static bool BeValidPhoneNumber(string phoneNumber)
	{
		if (string.IsNullOrWhiteSpace(phoneNumber))
			return false;

		var numbersOnly = Replace(phoneNumber, @"\D", "");

		if (numbersOnly.Length is < 10 or > 11)
			return false;

		var patterns = new[]
		{
			@"^\(\d{2}\)\s?\d{4,5}-\d{4}$",
			@"^\d{2}\s\d{4,5}-\d{4}$",
			@"^\d{10,11}$"
		};

		return patterns.Any(pattern => IsMatch(phoneNumber, pattern));
	}
}