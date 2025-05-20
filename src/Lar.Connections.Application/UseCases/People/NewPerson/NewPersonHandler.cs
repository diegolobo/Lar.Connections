using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Entities;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.NewPerson;

public class NewPersonHandler : IRequestHandler<NewPersonCommand, Result<NewPersonResult>>
{
	private readonly IPersonRepository _repository;

	public NewPersonHandler(IPersonRepository personRepository)
	{
		_repository = personRepository;
	}

	public async Task<Result<NewPersonResult>> Handle(NewPersonCommand request, CancellationToken cancellationToken)
	{
		var existentPerson = await _repository.GetByDocumentAsync(request.Document);

		if (existentPerson is not null)
			return Result<NewPersonResult>.EntityAlreadyExists(
				nameof(Person),
				request.Document,
				"Document already registered");

		var person = Person.Create(request.Name, request.Document, request.BirthDate, true);

		var personId = await _repository.CreateAsync(person);

		if (personId < 1)
			return Result<NewPersonResult>.WithError("Unable to save person, please try again later");

		person.Id = personId;

		if (request.Phones.Count <= 0) return new NewPersonResult(person.Id);

		foreach (var phone in request.Phones.Select(phoneRequest =>
					 Phone.Create(personId, phoneRequest.Number, phoneRequest.Type)))
		{
			await _repository.AddPhoneAsync(personId, phone);
			person.Phones.Add(phone);
		}

		return new NewPersonResult(person.Id);
	}
}