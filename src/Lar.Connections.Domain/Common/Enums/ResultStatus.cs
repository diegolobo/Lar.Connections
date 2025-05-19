namespace Lar.Connections.Domain.Common.Enums;

public enum ResultStatus
{
	Success,
	HasValidation,
	HasError,
	EntityNotFound,
	EntityAlreadyExists,
	NoContent
}