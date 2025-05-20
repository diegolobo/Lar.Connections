using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.Deactivate;

public record DeactivateCommand(long Id) : IRequest<Result<DeactivateResult>>;