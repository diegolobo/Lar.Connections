using FluentValidation;

using Lar.Connections.Domain.Entities;

namespace Lar.Connections.Application.UseCases.People.GetPeople;

public class GetPeopleValidator : AbstractValidator<GetPeopleQuery>
{
	public GetPeopleValidator()
	{
		RuleFor(x => x.Page)
			.GreaterThan(0)
			.WithMessage("Page must be greater than 0.");

		RuleFor(x => x.PageSize)
			.GreaterThan(0)
			.LessThanOrEqualTo(100)
			.WithMessage("PageSize must be between 1 and 100.");

		RuleFor(x => x.Id)
			.GreaterThan(0)
			.When(x => x.Id.HasValue)
			.WithMessage("Id must be greater than 0 when provided.");

		RuleFor(x => x.Document)
			.MinimumLength(3)
			.When(x => !string.IsNullOrWhiteSpace(x.Document))
			.WithMessage("Document must have at least 3 characters when provided.");

		RuleFor(x => x.SearchTerm)
			.MinimumLength(2)
			.When(x => !string.IsNullOrWhiteSpace(x.SearchTerm))
			.WithMessage("Search term must have at least 2 characters when provided.");

		RuleFor(x => x.SortBy)
			.Must(BeValidSortField)
			.When(x => !string.IsNullOrWhiteSpace(x.SortBy))
			.WithMessage("SortBy must be one of: Name, Document, BirthDate, Id.");
	}

	private static bool BeValidSortField(string? sortBy)
	{
		if (string.IsNullOrWhiteSpace(sortBy))
			return true;

		string[] validFields =
		[
			nameof(Person.Id),
			nameof(Person.Name),
			nameof(Person.Document),
			nameof(Person.BirthDate)
		];

		return validFields.Contains(sortBy.ToLower());
	}
}