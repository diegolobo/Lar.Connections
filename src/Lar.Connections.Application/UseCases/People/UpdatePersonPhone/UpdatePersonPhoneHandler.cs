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
		var existingPerson = await _repository.GetByIdAsync(request.GetPersonId());

		if (existingPerson is null)
			return Result<UpdatePersonPhoneResult>.EntityNotFound(
				nameof(Person),
				request.GetPersonId(),
				"Person not found");

		var existingPhone = await _repository.GetPhonesByPersonIdAsync(request.GetPersonId());
		if (!existingPhone.Exists(p => p.Id == request.GetId()))
			return Result<UpdatePersonPhoneResult>.EntityNotFound(
				nameof(Phone),
				request.GetPersonId(),
				"Phone not found");

		return await _repository.UpdatePhoneAsync(
			request.GetPersonId(),
			Phone.Create(
				request.GetPersonId(),
				request.Number,
				request.Type))
			? new UpdatePersonPhoneResult(true)
			: Result<UpdatePersonPhoneResult>.WithError(
				"Unable to update phone for this person, please try again later");
	}
}