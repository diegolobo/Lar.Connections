using Lar.Connections.Domain.Common.Enums;

namespace Lar.Connections.Domain.Common.Interfaces;

public interface IResult
{
	ResultStatus Status { get; }
}

public interface IResult<out T> : IResult
{
	T? Data { get; }
}