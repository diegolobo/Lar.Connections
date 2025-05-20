using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.ExcludePerson;

public class ExcludePersonHandler : IRequestHandler<ExcludePersonCommand, Result<ExcludePersonResult>>
{
	private readonly IPersonRepository _repository;

	public ExcludePersonHandler(IPersonRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<ExcludePersonResult>> Handle(
		ExcludePersonCommand request,
		CancellationToken cancellationToken)
	{
		var existingPerson = await _repository.GetByIdAsync(request.Id);

		if (existingPerson is null)
			return Result<ExcludePersonResult>.EntityNotFound(nameof(Person), request.Id, "Person not found");

		return await _repository.DeleteAsync(request.Id)
			? new ExcludePersonResult(true)
			: Result<ExcludePersonResult>.WithError("Unable to exclude person, please try again later");
	}
}