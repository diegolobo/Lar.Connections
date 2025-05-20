using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.UpdatePerson;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, Result<UpdatePersonResult>>
{
	private readonly IPersonRepository _repository;

	public UpdatePersonHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<UpdatePersonResult>> Handle(
		UpdatePersonCommand request,
		CancellationToken cancellationToken)
	{
		var existingPerson = await _repository.GetByIdAsync(request.GetId());
		const bool active = true;

		if (existingPerson == null)
			return Result<UpdatePersonResult>.EntityNotFound(nameof(Person), request.GetId(), "Person not found");

		existingPerson.Update(request.GetId(), request.Name, request.Document, request.BirthDate, active);

		var success = await _repository.UpdateAsync(existingPerson);

		return !success
			? Result<UpdatePersonResult>.WithError("Unable to save person, please try again later")
			: new UpdatePersonResult(request.GetId(), request.Name, active);
	}
}