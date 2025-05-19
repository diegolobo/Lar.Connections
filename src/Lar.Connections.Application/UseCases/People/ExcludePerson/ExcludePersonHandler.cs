using Lar.Connections.Domain.Common;
using MediatR;

namespace Lar.Connections.Application.UseCases.People.ExcludePerson;

public class ExcludePersonHandler : IRequestHandler<ExcludePersonCommand, Result<ExcludePersonResult>>
{
	public Task<Result<ExcludePersonResult>> Handle(ExcludePersonCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}