using Dapper;

using Lar.Connections.Infrastructure.Constants;
using Lar.Connections.Infrastructure.Queries;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using Serilog;

namespace Lar.Connections.Infrastructure.Seeds;

public class DatabaseInitializer : IDatabaseInitializer
{
	private readonly string _connectionString;

	private readonly HashSet<TableItem> _initializedTables =
	[
		new(PersonQueries.TableName, PersonQueries.CreateTable),
		new(PhoneQueries.TableName, PhoneQueries.CreateTable)
	];

	public DatabaseInitializer(
		IConfiguration configuration,
		string connectionStringName = DatabaseConstants.DefaultConnectionName)
	{
		var connectionString = configuration.GetConnectionString(connectionStringName);

		if (string.IsNullOrEmpty(connectionString))
			throw new ArgumentNullException(DatabaseConstants.EmptyConnectionStringErrorMessage);

		_connectionString = connectionString;
	}

	public async Task InitializeAsync()
	{
		try
		{
			foreach (var item in _initializedTables)
				await EnsureTableExistsAsync(item.TableName, item.CreateTable);
		}
		catch (Exception e)
		{
			const string errorMessage =
				"Erro ao inicializar o banco de dados. Favor verificar a configuração da Connection string";
			Log.Logger.Error(e, errorMessage);
		}
	}

	public async Task EnsureTableExistsAsync(string tableName, string createTableQuery)
	{
		try
		{
			await using var connection = new SqlConnection(_connectionString);
			await connection.OpenAsync();

			var checkTableQuery =
				$"""
				 	SELECT COUNT(*) 
				 	FROM sys.tables 
				 	WHERE 1 = 1
				 	  AND [name] = '{tableName}';
				 """;

			var tableExists = await connection.ExecuteScalarAsync<int>(checkTableQuery);

			if (tableExists == 0)
			{
				await connection.ExecuteAsync(createTableQuery);
				Log.Logger.Information($"Tabela {tableName} criada com sucesso");
			}
		}
		catch (Exception ex)
		{
			var errorMessage = $"Erro ao verificar/criar a tabela {tableName}";
			Log.Logger.Error(ex, errorMessage);
			throw;
		}
	}
}