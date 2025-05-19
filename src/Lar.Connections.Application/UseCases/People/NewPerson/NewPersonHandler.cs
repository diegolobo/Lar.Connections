using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.NewPerson;

public class NewPersonHandler : IRequestHandler<NewPersonCommand, Result<NewPersonResult>>
{
	public Task<Result<NewPersonResult>> Handle(NewPersonCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}