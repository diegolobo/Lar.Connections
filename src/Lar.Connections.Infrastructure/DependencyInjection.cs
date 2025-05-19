using Lar.Connections.Application.UseCases.People;
using Lar.Connections.Infrastructure.Repositories;
using Lar.Connections.Infrastructure.Seeds;

using Microsoft.Extensions.DependencyInjection;

namespace Lar.Connections.Infrastructure;

public static class DependencyInjection
{
	public static void AddDatabase(this IServiceCollection services)
	{
		services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
		services.AddScoped<IPersonRepository, PersonRepository>();
	}
}