namespace Lar.Connections.Infrastructure.Seeds;

public interface IDatabaseInitializer
{
	Task InitializeAsync();
}