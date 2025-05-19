using FluentValidation;

using Lar.Connections.Application.Behaviors;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

namespace Lar.Connections.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		var assembly = Assembly.GetExecutingAssembly();
		services.AddValidatorsFromAssembly(assembly);
		services.AddMediatR(assembly);

		services.AddScoped(
			typeof(IPipelineBehavior<,>),
			typeof(ValidationBehavior<,>));

		return services;
	}
}