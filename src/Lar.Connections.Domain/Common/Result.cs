using Lar.Connections.Domain.Common.Enums;
using Lar.Connections.Domain.Common.Interfaces;
using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common;

public class Result<T> : ResultBase, IResult<T>
{
	public static Result<T> Success(T data)
	{
		return new Result<T>
		{
			Data = data,
			Status = ResultStatus.Success
		};
	}

	public new static Result<T> WithNoContent()
	{
		return new Result<T>
		{
			Status = ResultStatus.NoContent
		};
	}

	public new static Result<T> EntityNotFound(string entityName, object id, string message)
	{
		return new Result<T>
		{
			Status = ResultStatus.EntityNotFound,
			EntityWarning = new EntityWarning(entityName, id, message)
		};
	}

	public new static Result<T> EntityAlreadyExists(string entityName, object id, string message)
	{
		return new Result<T>
		{
			Status = ResultStatus.EntityAlreadyExists,
			EntityWarning = new EntityWarning(entityName, id, message)
		};
	}

	public new static Result<T> WithError(string message)
	{
		return new Result<T>
		{
			Status = ResultStatus.HasError,
			Error = new Error(message)
		};
	}

	public new static Result<T> WithError(Exception exception)
	{
		return WithError(exception.Message);
	}

	public new static Result<T> WithValidations(params Validation[] validations)
	{
		return new Result<T>
		{
			Status = ResultStatus.HasValidation,
			Validations = validations
		};
	}

	public new static Result<T> WithValidations(string propertyName, string description)
	{
		return WithValidations(new Validation(propertyName, description));
	}

	public T? Data { get; private init; }

	public static implicit operator Result<T>(T data)
	{
		return Success(data);
	}

	public static implicit operator Result<T>(Exception ex)
	{
		return WithError(ex);
	}

	public static implicit operator Result<T>(Validation[] validations)
	{
		return WithValidations(validations);
	}

	public static implicit operator Result<T>(Validation validation)
	{
		return WithValidations(validation);
	}
}