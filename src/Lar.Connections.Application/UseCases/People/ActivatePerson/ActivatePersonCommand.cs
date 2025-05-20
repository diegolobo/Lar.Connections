using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.ActivatePerson;

public record ActivatePersonCommand(long Id) : IRequest<Result<ActivatePersonResult>>;