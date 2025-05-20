using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.UpdatePersonPhone;

public class UpdatePersonPhoneHandler : IRequestHandler<UpdatePersonPhoneCommand, Result<UpdatePersonPhoneResult>>
{
	private readonly IPersonRepository _repository;

	public UpdatePersonPhoneHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<UpdatePersonPhoneResult>> Handle(
		UpdatePersonPhoneCommand request,
		CancellationToken cancellationToken)
	{
		var existingPerson = await _repository.GetByIdAsync(request.PersonId);

		if (existingPerson is null)
			return Result<UpdatePersonPhoneResult>.EntityNotFound(nameof(Person), request.PersonId, "Person not found");

		var existingPhone = await _repository.GetPhonesByPersonIdAsync(request.PersonId);
		if (!existingPhone.Exists(p => p.Id == request.Id))
			return Result<UpdatePersonPhoneResult>.EntityNotFound(nameof(Phone), request.PersonId, "Phone not found");

		return await _repository.UpdatePhoneAsync(
			request.PersonId,
			Phone.Create(
				request.PersonId,
				request.Number,
				request.Type))
			? new UpdatePersonPhoneResult(true)
			: Result<UpdatePersonPhoneResult>.WithError("Unable to exclude person, please try again later");
	}
}