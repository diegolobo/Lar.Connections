using Lar.Connections.Domain.Common.Enums;
using Lar.Connections.Domain.Common.Interfaces;
using Lar.Connections.Domain.Common.Records;

namespace Lar.Connections.Domain.Common;

public class Result<T> : ResultBase, IResult<T>
{
	/// <summary>
	/// Create a Success Result
	/// </summary>
	/// <param name="data">Data</param>
	/// <returns>Success Result</returns>
	public static Result<T> Success(T data)
	{
		return new Result<T>
		{
			Data = data,
			Status = ResultStatus.Success
		};
	}

	/// <summary>
	/// Create a No Content Result
	/// </summary>
	/// <returns>No Content Result</returns>
	public new static Result<T> WithNoContent()
	{
		return new Result<T>
		{
			Status = ResultStatus.NoContent
		};
	}

	/// <summary>
	/// Create a Entity Not Found Result
	/// </summary>
	/// <param name="entity">Entity</param>
	/// <param name="id">Entity Id</param>
	/// <param name="description">Description</param>
	/// <returns>Entity Not Found Result</returns>
	public new static Result<T> EntityNotFound(string entity, object id, string description)
	{
		return new Result<T>
		{
			Status = ResultStatus.EntityNotFound,
			EntityWarning = new EntityWarning(entity, id, description)
		};
	}

	/// <summary>
	/// Create a Entity Already Exists Result
	/// </summary>
	/// <param name="entity">Entity</param>
	/// <param name="id">Entity Id</param>
	/// <param name="description">Description</param>
	/// <returns>Entity Already Exists Result</returns>
	public new static Result<T> EntityAlreadyExists(string entity, object id, string description)
	{
		return new Result<T>
		{
			Status = ResultStatus.EntityAlreadyExists,
			EntityWarning = new EntityWarning(entity, id, description)
		};
	}

	/// <summary>
	/// Create an Error Result
	/// </summary>
	/// <param name="message">Error Message</param>
	/// <returns>Error Result</returns>
	public new static Result<T> WithError(string message)
	{
		return new Result<T>
		{
			Status = ResultStatus.HasError,
			Error = new Error(message)
		};
	}

	/// <summary>
	/// Create an Error Result
	/// </summary>
	/// <param name="exception">Exception</param>
	/// <returns>Error Result</returns>
	public new static Result<T> WithError(Exception exception)
	{
		return WithError(exception.Message);
	}

	/// <summary>
	/// Create a Validation Result
	/// </summary>
	/// <param name="validations">Validations List</param>
	/// <returns>Validation Result</returns>
	public new static Result<T> WithValidations(params Validation[] validations)
	{
		return new Result<T>
		{
			Status = ResultStatus.HasValidation,
			Validations = validations
		};
	}

	/// <summary>
	/// Create a Validation Result
	/// </summary>
	/// <param name="propertyName">Property Name</param>
	/// <param name="description">Description</param>
	/// <returns></returns>
	public new static Result<T> WithValidations(string propertyName, string description)
	{
		return WithValidations(new Validation(propertyName, description));
	}

	/// <summary>
	/// Data
	/// </summary>
	public T? Data { get; private init; }

	/// <summary>
	/// Create a Success Result with Data
	/// </summary>
	/// <param name="data">Data</param>
	/// <returns>Success Result</returns>
	public static implicit operator Result<T>(T data)
	{
		return Success(data);
	}

	/// <summary>
	/// Create a Error Result with Exception
	/// </summary>
	/// <param name="ex">Exception</param>
	/// <returns>Error Result</returns>
	public static implicit operator Result<T>(Exception ex)
	{
		return WithError(ex);
	}

	/// <summary>
	/// Create a Validation Result with Validations List
	/// </summary>
	/// <param name="validations">Validations List</param>
	/// <returns>Validation Result</returns>
	public static implicit operator Result<T>(Validation[] validations)
	{
		return WithValidations(validations);
	}

	/// <summary>
	/// Create a Validation Result with Validation
	/// </summary>
	/// <param name="validation">Validation</param>
	/// <returns>Validation Result</returns>
	public static implicit operator Result<T>(Validation validation)
	{
		return WithValidations(validation);
	}
}