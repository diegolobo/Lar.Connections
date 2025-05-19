using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.UpdatePerson;

public record UpdatePersonCommand(
	long Id,
	string Name,
	string Document,
	DateTime BirthDate)
	: IRequest<Result<UpdatePersonResult>>;