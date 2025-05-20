using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Common.Enums;
using Lar.Connections.Domain.Common.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using AspNetResult = Microsoft.AspNetCore.Http.IResult;

namespace Lar.Connections.WebApi.Controllers.Base;

[ApiController]
public class ApiController : ControllerBase
{
	protected readonly IMediator Mediator;

	public ApiController(IMediator mediator)
	{
		Mediator = mediator;
	}
}

public static class SenderResult
{
	public static async Task<AspNetResult> Send<TRequest, TResponse>(
		IMediator mediator,
		TRequest command,
		CancellationToken cancellationToken)
		where TRequest : IRequest<Result<TResponse>>
	{
		try
		{
			var result = await mediator.Send(command, cancellationToken);
			return result.Status switch
			{
				ResultStatus.HasError => Results.Problem(),
				ResultStatus.HasValidation => Results.BadRequest(result.Validations),
				_ => Results.Ok(result.Data)
			};
		}
		catch (ResultException e)
		{
			return Results.BadRequest(e.Result.Validations);
		}
		catch (Exception)
		{
			return Results.Problem();
		}
	}
}