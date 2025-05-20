using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.DeletePersonPhone;

public record DeletePersonPhoneCommand(long PersonId, long Id) : IRequest<Result<DeletePersonPhoneResult>>;