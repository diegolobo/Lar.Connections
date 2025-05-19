using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Enums;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.NewPerson;

public record NewPersonCommand(
	string Name,
	string Document,
	DateTime BirthDate,
	List<NewPersonPhone> Phones)
	: IRequest<Result<NewPersonResult>>;

public record NewPersonPhone(PhoneType Type, string Number);