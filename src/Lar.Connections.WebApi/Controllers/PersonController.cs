using Lar.Connections.Application.UseCases.People.ExcludePerson;
using Lar.Connections.Application.UseCases.People.GetPeople;
using Lar.Connections.Application.UseCases.People.NewPerson;
using Lar.Connections.Application.UseCases.People.UpdatePerson;
using Lar.Connections.Domain.Entities;
using Lar.Connections.WebApi.Controllers.Base;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace Lar.Connections.WebApi.Controllers;

[Route("api/[controller]")]
public class PersonController : ApiController
{
	public PersonController(IMediator mediator)
		: base(mediator)
	{
	}

	[HttpGet]
	public async Task<IResult> GetAll(
		[FromQuery] int page = 1,
		[FromQuery] int pageSize = 10,
		[FromQuery] string? searchTerm = null,
		[FromQuery] bool includeInactive = false,
		[FromQuery] bool includePhones = true,
		[FromQuery] string? sortBy = nameof(Person.Name),
		[FromQuery] bool sortDescending = false,
		CancellationToken cancellationToken = default)
	{
		var query = new GetPeopleQuery
		{
			Page = page,
			PageSize = pageSize,
			SearchTerm = searchTerm,
			IncludeInactive = includeInactive,
			IncludePhones = includePhones,
			SortBy = sortBy,
			SortDescending = sortDescending
		};

		return await SenderResult.Send<GetPeopleQuery, GetPeopleResult>(
			Mediator,
			query,
			cancellationToken);
	}

	[HttpGet("{id}")]
	public async Task<IResult> GetById(
		long id,
		[FromQuery] bool includePhones = true,
		CancellationToken cancellationToken = default)
	{
		var query = new GetPeopleQuery
		{
			Id = id,
			IncludePhones = includePhones
		};

		return await SenderResult.Send<GetPeopleQuery, GetPeopleResult>(
			Mediator,
			query,
			cancellationToken);
	}

	[HttpGet("by-document/{document}")]
	public async Task<IResult> GetByDocument(
		string document,
		[FromQuery] bool includePhones = true,
		CancellationToken cancellationToken = default)
	{
		var query = new GetPeopleQuery
		{
			Document = document,
			IncludePhones = includePhones
		};

		return await SenderResult.Send<GetPeopleQuery, GetPeopleResult>(
			Mediator,
			query,
			cancellationToken);
	}

	[HttpGet("search/{term}")]
	public async Task<IResult> Search(
		string term,
		[FromQuery] int page = 1,
		[FromQuery] int pageSize = 10,
		[FromQuery] bool includeInactive = false,
		[FromQuery] bool includePhones = true,
		CancellationToken cancellationToken = default)
	{
		var query = new GetPeopleQuery
		{
			IncludeInactive = includeInactive,
			IncludePhones = includePhones,
			SearchTerm = term,
			Page = page,
			PageSize = pageSize
		};

		return await SenderResult.Send<GetPeopleQuery, GetPeopleResult>(
			Mediator,
			query,
			cancellationToken);
	}

	[HttpPost]
	public async Task<IResult> Create(
		[FromBody] NewPersonCommand command,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<NewPersonCommand, NewPersonResult>(
			Mediator,
			command,
			cancellationToken);
	}

	[HttpPut("{id}")]
	public async Task<IResult> Update(
		long id,
		[FromBody] UpdatePersonCommand command,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<UpdatePersonCommand, UpdatePersonResult>(
			Mediator,
			command.SetId(id),
			cancellationToken);
	}

	[HttpDelete("{id}")]
	public async Task<IResult> Delete(
		long id,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<ExcludePersonCommand, ExcludePersonResult>(
			Mediator,
			new ExcludePersonCommand(id),
			cancellationToken);
	}
}