using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.UpdatePerson;

public class UpdatePersonCommand : IRequest<Result<UpdatePersonResult>>
{
	private long _id;
	public string Name { get; set; } = string.Empty;
	public string Document { get; set; } = string.Empty;
	public DateTime BirthDate { get; set; }

	public UpdatePersonCommand SetId(long id)
	{
		_id = id;
		return this;
	}

	public long GetId()
	{
		return _id;
	}
}