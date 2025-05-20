using Lar.Connections.Domain.Entities;
using Lar.Connections.Infrastructure.Utilities;

namespace Lar.Connections.Infrastructure.Queries;

internal static class PhoneQueries
{
	private const string FileName = "CREATE TABLE Phones.sql";
	internal const string TableName = "Phones";

	internal static readonly string CreateTable =
		SqlScriptLoader.LoadSqlScript(FileName);

	internal const string GetByPersonId =
		$"""
		 	SELECT Id AS {nameof(Phone.Id)}
		 			, Number AS {nameof(Phone.Number)}
		 			, Type AS {nameof(Phone.Type)}
		 			, PersonId AS {nameof(Phone.PersonId)}
		 	FROM {TableName} 
		 	WHERE 1 = 1
		 	  AND PersonId = @PersonId 
		 	ORDER BY Id
		 """;

	internal const string Insert =
		$"""
		 	INSERT INTO {TableName} (Number, Type, PersonId)
		 	OUTPUT INSERTED.Id
		 	VALUES (@Number, @Type, @PersonId)
		 """;

	internal const string Update =
		$"""
		 	UPDATE {TableName} SET 
		 		Number = @Number
		 		, Type = @Type
		 	WHERE Id = @Id 
		 	  AND PersonId = @PersonId
		 """;

	internal const string DeleteByPersonId =
		$"""
		 	DELETE FROM {TableName} 
		 	WHERE PersonId = @PersonId
		 """;

	internal const string Delete =
		$"""
		  	DELETE FROM {TableName}
		 	WHERE PersonId = @id
		 """;
}