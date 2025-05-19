using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common.Exceptions;

public class ResultException(Result result) : Exception
{
	public Result Result => result;

	public ResultException(params Validation[] validations)
		: this(Result.WithValidations(validations))
	{
	}

	public ResultException(Exception exception)
		: this(Result.WithError(exception))
	{
	}
}