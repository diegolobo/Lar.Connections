using Lar.Connections.Domain.Common.Enums;
using Lar.Connections.Domain.Common.Interfaces;
using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common;

public class ResultBase : IResultValidations, IResultError, IRequestEntityWarning
{
	public static ResultBase Success()
	{
		return new ResultBase { Status = ResultStatus.Success };
	}

	public static ResultBase WithNoContent()
	{
		return new ResultBase { Status = ResultStatus.NoContent };
	}

	public static ResultBase EntityNotFound(string entity, object id, string description)
	{
		return new ResultBase
		{
			Status = ResultStatus.EntityNotFound,
			EntityWarning = new EntityWarning(entity, id, description)
		};
	}

	public static ResultBase EntityAlreadyExists(string entity, object id, string description)
	{
		return new ResultBase
		{
			Status = ResultStatus.EntityAlreadyExists,
			EntityWarning = new EntityWarning(entity, id, description)
		};
	}

	public static ResultBase WithError(string message)
	{
		return new ResultBase
		{
			Status = ResultStatus.HasError,
			Error = new Error(message)
		};
	}

	public static ResultBase WithError(Exception exception)
	{
		return WithError(exception.Message);
	}

	public static ResultBase WithValidations(params Validation[] validations)
	{
		return new ResultBase
		{
			Status = ResultStatus.HasValidation,
			Validations = validations
		};
	}

	public static ResultBase WithValidations(IEnumerable<Validation> validations)
	{
		return WithValidations(validations.ToArray());
	}

	public static ResultBase WithValidations(string propertyName, string description)
	{
		return WithValidations(new Validation(propertyName, description));
	}

	public ResultStatus Status { get; protected init; }

	public IEnumerable<Validation> Validations { get; protected init; } = [];

	public Error? Error { get; protected init; }

	public EntityWarning? EntityWarning { get; protected init; }
}