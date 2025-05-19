using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common.Interfaces;

public interface IResultValidations : IResult
{
	IEnumerable<Validation> Validations { get; }
}