using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Enums;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.UpdatePersonPhone;

public record UpdatePersonPhoneCommand(long PersonId, long Id, string Number, PhoneType Type)
	: IRequest<Result<UpdatePersonPhoneResult>>;