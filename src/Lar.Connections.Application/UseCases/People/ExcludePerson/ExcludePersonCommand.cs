using Lar.Connections.Domain.Common;

using MediatR;

namespace Lar.Connections.Application.UseCases.People.ExcludePerson;

public record ExcludePersonCommand(long Id) : IRequest<Result<ExcludePersonResult>>;