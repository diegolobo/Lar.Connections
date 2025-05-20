using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Enums;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.AddPersonPhone;

public record AddPersonPhoneCommand(
	long PersonId,
	PhoneType Type,
	string Number)
	: IRequest<Result<AddPersonPhoneResult>>;