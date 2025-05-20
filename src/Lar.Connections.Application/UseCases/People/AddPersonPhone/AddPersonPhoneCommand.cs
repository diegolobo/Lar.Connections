using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Enums;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.AddPersonPhone;

public record AddPersonPhoneCommand(
	PhoneType Type,
	string Number)
	: IRequest<Result<AddPersonPhoneResult>>
{
	private long _personId;

	public AddPersonPhoneCommand SetPersonId(long personId)
	{
		_personId = personId;
		return this;
	}

	public long GetPersonId()
	{
		return _personId;
	}
}