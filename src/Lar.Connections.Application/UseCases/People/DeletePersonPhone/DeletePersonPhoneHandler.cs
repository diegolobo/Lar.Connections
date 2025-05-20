using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.DeletePersonPhone;

public class DeletePersonPhoneHandler : IRequestHandler<DeletePersonPhoneCommand, Result<DeletePersonPhoneResult>>
{
	private readonly IPersonRepository _repository;

	public DeletePersonPhoneHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<DeletePersonPhoneResult>> Handle(
		DeletePersonPhoneCommand request,
		CancellationToken cancellationToken)
	{
		var existingPerson = await _repository.GetByIdAsync(request.Id);

		if (existingPerson is null)
			return Result<DeletePersonPhoneResult>.EntityNotFound(nameof(Person), request.Id, "Person not found");

		var existingPhone = await _repository.GetPhonesByPersonIdAsync(request.PersonId);
		if (existingPhone.All(x => x.Id != request.Id))
			return Result<DeletePersonPhoneResult>.EntityNotFound(nameof(Phone), request.Id, "Phone not found");

		return await _repository.DeletePhoneAsync(request.Id)
			? new DeletePersonPhoneResult(true)
			: Result<DeletePersonPhoneResult>.WithError(
				"Unable to exclude phone for this person, please try again later");
	}
}