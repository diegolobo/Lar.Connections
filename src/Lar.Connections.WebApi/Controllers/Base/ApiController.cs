using Lar.Connections.Domain.Common;
using Lar.Connections.Domain.Common.Enums;
using Lar.Connections.Domain.Common.Exceptions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

using AspNetResult = Microsoft.AspNetCore.Http.IResult;

namespace Lar.Connections.WebApi.Controllers.Base;

[ApiController]
public class ApiController : ControllerBase
{
	protected readonly IMediator Mediator;
	protected readonly IOutputCacheStore OutputCacheStore;

	public ApiController(IMediator mediator, IOutputCacheStore outputCacheStore)
	{
		Mediator = mediator;
		OutputCacheStore = outputCacheStore;
	}
}

public static class SenderResult
{
	public static async Task<AspNetResult> Send<TRequest, TResponse>(
		IMediator mediator,
		IOutputCacheStore outputCacheStore,
		TRequest command,
		CancellationToken cancellationToken,
		bool evict = false)
		where TRequest : IRequest<Result<TResponse>>
	{
		try
		{
			var result = await mediator.Send(command, cancellationToken);
			return result.Status switch
			{
				ResultStatus.HasError => Results.Problem(),
				ResultStatus.HasValidation => Results.BadRequest(result.Validations),
				_ => await SendSuccess<TRequest, TResponse>(
					outputCacheStore,
					result,
					cancellationToken,
					evict)
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

	private static async Task<AspNetResult> SendSuccess<TRequest, TResponse>(
		IOutputCacheStore outputCacheStore,
		Result<TResponse> result,
		CancellationToken cancellationToken,
		bool evict = false) where TRequest : IRequest<Result<TResponse>>
	{
		if (evict) await outputCacheStore.EvictByTagAsync("people", cancellationToken);

		return Results.Ok(result.Data);
	}
}