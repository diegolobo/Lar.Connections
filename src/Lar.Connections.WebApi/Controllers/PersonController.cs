using Lar.Connections.Application.UseCases.People.ExcludePerson;
using Lar.Connections.Application.UseCases.People.NewPerson;
using Lar.Connections.Application.UseCases.People.UpdatePerson;
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