using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common.Interfaces;

public interface IRequestEntityWarning : IResult
{
	EntityWarning? EntityWarning { get; }
}