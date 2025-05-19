namespace Lar.Connections.Infrastructure.Utilities;

public static class SqlScriptLoader
{
	private const string ScriptsDirectory = "Scripts";

	public static string LoadSqlScript(string fileName)
	{
		var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		var scriptPath = Path.Combine(baseDirectory, ScriptsDirectory, fileName);

		if (!File.Exists(scriptPath))
			scriptPath = Path.Combine(Directory.GetCurrentDirectory(), ScriptsDirectory, fileName);

		if (!File.Exists(scriptPath))
			throw new FileNotFoundException($"O arquivo SQL '{fileName}' não foi encontrado.", scriptPath);

		return File.ReadAllText(scriptPath);
	}
}