using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.UpdatePerson;

public class UpdatePersonHandler : IRequestHandler<UpdatePersonCommand, Result<UpdatePersonResult>>
{
	public Task<Result<UpdatePersonResult>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}