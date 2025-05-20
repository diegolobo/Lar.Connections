using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.AddPersonPhone;

public class AddPersonPhoneHandler : IRequestHandler<AddPersonPhoneCommand, Result<AddPersonPhoneResult>>
{
	private readonly IPersonRepository _repository;

	public AddPersonPhoneHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<AddPersonPhoneResult>> Handle(
		AddPersonPhoneCommand request,
		CancellationToken cancellationToken)
	{
		var existingPerson = await _repository.GetByIdAsync(request.GetPersonId());

		if (existingPerson is null)
			return Result<AddPersonPhoneResult>.EntityNotFound(nameof(Person), request.GetPersonId(),
				"Person not found");

		return await _repository.AddPhoneAsync(
			request.GetPersonId(),
			Phone.Create(
				request.GetPersonId(),
				request.Number,
				request.Type))
			? new AddPersonPhoneResult(true)
			: Result<AddPersonPhoneResult>.WithError("Unable to add phone for this person, please try again later");
	}
}