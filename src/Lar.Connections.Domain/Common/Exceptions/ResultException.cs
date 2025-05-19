using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common.Exceptions;

public class ResultException(ResultBase result) : Exception
{
	public ResultBase Result => result;

	public ResultException(params Validation[] validations)
		: this(ResultBase.WithValidations(validations))
	{
	}

	public ResultException(Exception exception)
		: this(ResultBase.WithError(exception))
	{
	}
}