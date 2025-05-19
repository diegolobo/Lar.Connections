using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common.Interfaces;

public interface IResultError : IResult
{
	/// <summary>
	/// Error
	/// </summary>
	Error? Error { get; }
}