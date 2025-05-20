using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.ActivatePerson;

public class ActivatePersonHandler : IRequestHandler<ActivatePersonCommand, Result<ActivatePersonResult>>
{
	private readonly IPersonRepository _repository;

	public ActivatePersonHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<ActivatePersonResult>> Handle(
		ActivatePersonCommand request,
		CancellationToken cancellationToken)
	{
		var existingPerson = await _repository.GetByIdAsync(request.Id);

		if (existingPerson is null)
			return Result<ActivatePersonResult>.EntityNotFound(nameof(Person), request.Id, "Person not found");

		return await _repository.ActivateAsync(request.Id)
			? new ActivatePersonResult(true)
			: Result<ActivatePersonResult>.WithError("Unable to activate this person, please try again later");
	}
}