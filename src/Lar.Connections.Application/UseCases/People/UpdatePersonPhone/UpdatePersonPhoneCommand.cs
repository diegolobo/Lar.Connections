using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Enums;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.UpdatePersonPhone;

public record UpdatePersonPhoneCommand(string Number, PhoneType Type)
	: IRequest<Result<UpdatePersonPhoneResult>>
{
	private long _id;
	private long _personId;

	public UpdatePersonPhoneCommand SetPersonId(long personId)
	{
		_personId = personId;
		return this;
	}

	public long GetPersonId()
	{
		return _personId;
	}

	public UpdatePersonPhoneCommand SetId(long id)
	{
		_id = id;
		return this;
	}

	public long GetId()
	{
		return _id;
	}
}