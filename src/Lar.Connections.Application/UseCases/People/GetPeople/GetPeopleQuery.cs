using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.GetPeople;

public class GetPeopleQuery : IRequest<Result<GetPeopleResult>>
{
	public int Page { get; set; } = 1;
	public int PageSize { get; set; } = 10;
	public string? SearchTerm { get; set; }
	public string? Document { get; set; }
	public long? Id { get; set; }
	public bool IncludeInactive { get; set; } = false;
	public bool IncludePhones { get; set; } = true;
	public string? SortBy { get; set; } = nameof(Person.Name);
	public bool SortDescending { get; set; } = false;
}