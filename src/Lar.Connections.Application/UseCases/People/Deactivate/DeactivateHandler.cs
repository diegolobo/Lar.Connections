using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.Deactivate;

public class DeactivateHandler : IRequestHandler<DeactivateCommand, Result<DeactivateResult>>
{
	private readonly IPersonRepository _repository;

	public DeactivateHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<DeactivateResult>> Handle(
		DeactivateCommand request,
		CancellationToken cancellationToken)
	{
		var existingPerson = await _repository.GetByIdAsync(request.Id);

		if (existingPerson is null)
			return Result<DeactivateResult>.EntityNotFound(nameof(Person), request.Id, "Person not found");

		return await _repository.DeactivateAsync(request.Id)
			? new DeactivateResult(true)
			: Result<DeactivateResult>.WithError("Unable to deactivate person, please try again later");
	}
}