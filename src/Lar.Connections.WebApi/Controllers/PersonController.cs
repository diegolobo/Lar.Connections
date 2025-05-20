using Lar.Connections.Application.UseCases.People.ActivatePerson;
using Lar.Connections.Application.UseCases.People.AddPersonPhone;
using Lar.Connections.Application.UseCases.People.Deactivate;
using Lar.Connections.Application.UseCases.People.DeletePersonPhone;
using Lar.Connections.Application.UseCases.People.ExcludePerson;
using Lar.Connections.Application.UseCases.People.GetPeople;
using Lar.Connections.Application.UseCases.People.NewPerson;
using Lar.Connections.Application.UseCases.People.UpdatePerson;
using Lar.Connections.Application.UseCases.People.UpdatePersonPhone;
using Lar.Connections.Domain.Entities;
using Lar.Connections.WebApi.Controllers.Base;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace Lar.Connections.WebApi.Controllers;

[Route("api/[controller]")]
public class PersonController : ApiController
{
	public PersonController(IMediator mediator, IOutputCacheStore outputCacheStore)
		: base(mediator, outputCacheStore)
	{
	}

	[HttpGet]
	[OutputCache(PolicyName = "GetAllCache")]
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
			OutputCacheStore,
			query,
			cancellationToken);
	}

	[HttpGet("{id}")]
	[OutputCache(PolicyName = "PersonByIdCache")]
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
			OutputCacheStore,
			query,
			cancellationToken);
	}

	[HttpGet("by-document/{document}")]
	[OutputCache(PolicyName = "PersonByDocumentCache")]
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
			OutputCacheStore,
			query,
			cancellationToken);
	}

	[HttpGet("search/{term}")]
	[OutputCache(PolicyName = "SearchCache")]
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
			OutputCacheStore,
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
			OutputCacheStore,
			command,
			cancellationToken,
			true);
	}

	[HttpPut("{id}")]
	public async Task<IResult> Update(
		long id,
		[FromBody] UpdatePersonCommand command,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<UpdatePersonCommand, UpdatePersonResult>(
			Mediator,
			OutputCacheStore,
			command.SetId(id),
			cancellationToken,
			true);
	}

	[HttpPatch("{personId}/activate")]
	public async Task<IResult> Activate(
		long personId,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<ActivatePersonCommand, ActivatePersonResult>(
			Mediator,
			OutputCacheStore,
			new ActivatePersonCommand(personId),
			cancellationToken,
			true);
	}

	[HttpPatch("{personId}/add-phone")]
	public async Task<IResult> AddPhone(
		long personId,
		[FromBody] AddPersonPhoneCommand command,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<AddPersonPhoneCommand, AddPersonPhoneResult>(
			Mediator,
			OutputCacheStore,
			command.SetPersonId(personId),
			cancellationToken,
			true);
	}

	[HttpPatch("{personId}/deactivate")]
	public async Task<IResult> Deactivate(
		long personId,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<DeactivateCommand, DeactivateResult>(
			Mediator,
			OutputCacheStore,
			new DeactivateCommand(personId),
			cancellationToken,
			true);
	}

	[HttpPatch("{personId}/delete-phone/{id}")]
	public async Task<IResult> DeletePersonPhoneAsync(
		[FromRoute] long personId,
		[FromRoute] long id,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<DeletePersonPhoneCommand, DeletePersonPhoneResult>(
			Mediator,
			OutputCacheStore,
			new DeletePersonPhoneCommand(personId, id),
			cancellationToken,
			true);
	}

	[HttpPatch("{personId}/update-phone/{id}")]
	public async Task<IResult> UpdatePersonPhoneAsync(
		[FromRoute] long personId,
		[FromRoute] long id,
		[FromBody] UpdatePersonPhoneCommand command,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<UpdatePersonPhoneCommand, UpdatePersonPhoneResult>(
			Mediator,
			OutputCacheStore,
			command.SetPersonId(personId).SetId(id),
			cancellationToken,
			true);
	}

	[HttpDelete("{id}")]
	public async Task<IResult> Delete(
		long id,
		CancellationToken cancellationToken)
	{
		return await SenderResult.Send<ExcludePersonCommand, ExcludePersonResult>(
			Mediator,
			OutputCacheStore,
			new ExcludePersonCommand(id),
			cancellationToken,
			true);
	}
}