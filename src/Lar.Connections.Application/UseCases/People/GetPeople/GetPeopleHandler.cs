using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.GetPeople;

public class GetPeopleHandler : IRequestHandler<GetPeopleQuery, Result<GetPeopleResult>>
{
	private readonly IPersonRepository _repository;

	public GetPeopleHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<GetPeopleResult>> Handle(
		GetPeopleQuery request,
		CancellationToken cancellationToken)
	{
		try
		{
			if (request.Id.HasValue) return await GetPersonById(request.Id.Value, request.IncludePhones);

			if (!string.IsNullOrWhiteSpace(request.Document))
				return await GetPersonByDocument(request.Document, request.IncludePhones);

			return await GetPeopleWithPaginationOptimized(request);
		}
		catch (Exception ex)
		{
			return Result<GetPeopleResult>.WithError($"Error retrieving people: {ex.Message}");
		}
	}

	private async Task<Result<GetPeopleResult>> GetPersonById(long id, bool includePhones)
	{
		var person = includePhones
			? await _repository.GetPersonWithPhonesAsync(id)
			: await _repository.GetByIdAsync(id);

		if (person == null)
			return Result<GetPeopleResult>.EntityNotFound("Person", id, "Person not found");

		if (!includePhones && person.Phones.Any())
			person.Phones.Clear();

		var people = new List<PersonDto> { person };
		var result = new GetPeopleResult(people, 1, 1, 1);

		return Result<GetPeopleResult>.Success(result);
	}

	private async Task<Result<GetPeopleResult>> GetPersonByDocument(string document, bool includePhones)
	{
		var person = await _repository.GetByDocumentAsync(document);

		if (person == null)
			return Result<GetPeopleResult>.EntityNotFound("Person", document, "Person not found");

		if (includePhones)
		{
			var phones = await _repository.GetPhonesByPersonIdAsync(person.Id);
			person.Phones = phones;
		}

		var people = new List<PersonDto> { person };
		var result = new GetPeopleResult(people, 1, 1, 1);

		return Result<GetPeopleResult>.Success(result);
	}

	private async Task<Result<GetPeopleResult>> GetPeopleWithPaginationOptimized(GetPeopleQuery request)
	{
		var (people, totalCount) = await _repository.GetPeoplePagedAsync(
			request.Page,
			request.PageSize,
			request.SearchTerm,
			request.IncludeInactive,
			request.IncludePhones,
			request.SortBy,
			request.SortDescending);

		var peopleDto = people.Select(p => (PersonDto)p).ToList();

		var result = new GetPeopleResult(peopleDto, totalCount, request.Page, request.PageSize);
		return Result<GetPeopleResult>.Success(result);
	}
}