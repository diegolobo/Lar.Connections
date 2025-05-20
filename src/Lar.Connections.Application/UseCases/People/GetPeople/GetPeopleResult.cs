using Lar.Connections.Domain.Entities;
using Lar.Connections.Domain.Enums;

namespace Lar.Connections.Application.UseCases.People.GetPeople;

public class GetPeopleResult
{
	public List<PersonDto> People { get; set; } = [];
	public int TotalCount { get; set; }
	public int Page { get; set; }
	public int PageSize { get; set; }
	public int TotalPages { get; set; }
	public bool HasPreviousPage { get; set; }
	public bool HasNextPage { get; set; }

	public GetPeopleResult(List<PersonDto> people, int totalCount, int page, int pageSize)
	{
		People = people;
		TotalCount = totalCount;
		Page = page;
		PageSize = pageSize;
		TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
		HasPreviousPage = page > 1;
		HasNextPage = page < TotalPages;
	}
}

public class PersonDto
{
	public long Id { get; set; }
	public string? Name { get; set; }
	public string? Document { get; set; }
	public DateTime? BirthDate { get; set; }
	public bool Active { get; set; }
	public List<PhoneDto> Phones { get; set; } = [];

	public static implicit operator PersonDto(Person person)
	{
		return new PersonDto
		{
			Id = person.Id,
			Active = person.Active,
			Name = person.Name,
			Document = person.Document,
			BirthDate = person.BirthDate,
			Phones = person.Phones?.Select(p => new PhoneDto
			{
				Id = p.Id,
				Number = p.Number,
				Type = p.Type
			}).ToList() ?? []
		};
	}
}

public class PhoneDto
{
	public long Id { get; set; }
	public string? Number { get; set; }
	public PhoneType? Type { get; set; }
}