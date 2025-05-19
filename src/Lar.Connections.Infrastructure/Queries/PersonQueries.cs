using Lar.Connections.Domain.Entities;
using Lar.Connections.Infrastructure.Utilities;

namespace Lar.Connections.Infrastructure.Queries;

internal static class PersonQueries
{
	private const string FileName = "CREATE TABLE People.sql";
	internal const string TableName = "People";

	internal static readonly string CreateTable =
		SqlScriptLoader.LoadSqlScript(FileName);

	internal const string GetAll =
		$"""
		 	SELECT Id AS {nameof(Person.Id)}
		 			, Name AS {nameof(Person.Name)}
		 			, Document AS {nameof(Person.Document)}
		 			, BirthDate AS {nameof(Person.BirthDate)}
		 			, Active AS {nameof(Person.Active)}
		 	FROM {TableName} 
		 	ORDER BY Name
		 """;

	internal const string GetById =
		$"""
		     SELECT Id AS {nameof(Person.Id)}
		 			, Name AS {nameof(Person.Name)}
		 			, Document AS {nameof(Person.Document)}
		 			, BirthDate AS {nameof(Person.BirthDate)}
		 			, Active AS {nameof(Person.Active)} 
		     FROM {TableName}
		     WHERE Id = @Id
		 """;

	internal const string GetActive =
		$"""
		 	SELECT Id AS {nameof(Person.Id)}
		 			, Name AS {nameof(Person.Name)}
		 			, Document AS {nameof(Person.Document)}
		 			, BirthDate AS {nameof(Person.BirthDate)}
		 			, Active AS {nameof(Person.Active)}
		 	FROM {TableName} 
		 	WHERE Active = 1 
		 	ORDER BY Name
		 """;

	internal const string GetByDocument =
		$"""
		 	SELECT Id AS {nameof(Person.Id)}
		 			, Name AS {nameof(Person.Name)}
		 			, Document AS {nameof(Person.Document)}
		 			, BirthDate AS {nameof(Person.BirthDate)}
		 			, Active AS {nameof(Person.Active)}
		 	FROM {TableName} 
		 	WHERE Document = @Document
		 """;

	internal const string Insert =
		$"""
		     INSERT INTO {TableName} (Name, Document, BirthDate, Active)
		     OUTPUT INSERTED.Id
		     VALUES (@Name, @Document, @BirthDate, @Active)
		 """;

	internal const string Update =
		$"""
		     UPDATE {TableName} SET 
		 		Name = @Name, 
		         Document = @Document, 
		         BirthDate = @BirthDate, 
		         Active = @Active
		     WHERE Id = @Id
		 """;

	internal const string Delete =
		$"""
		     DELETE FROM {TableName} 
		     WHERE Id = @Id
		 """;

	internal const string Activate =
		$"""
		 	UPDATE {TableName} SET 
		 		Active = 1 
		 	WHERE Id = @Id
		 """;

	internal const string Deactivate =
		$"""
		     UPDATE {TableName} SET 
		 		Active = 0 
		     WHERE Id = @Id
		 """;

	internal const string GetPersonWithPhones =
		$"""
		     SELECT p.Id AS {nameof(Person.Id)}
		 			, p.Name AS {nameof(Person.Name)}
		 			, p.Document AS {nameof(Person.Document)}
		 			, p.BirthDate AS {nameof(Person.BirthDate)}
		 			, p.Active AS {nameof(Person.Active)}
		 			, ph.Id AS {nameof(Phone.Id)}
		 			, ph.Number AS {nameof(Phone.Number)}
		 			, ph.Type AS {nameof(Phone.Type)}
		 			, ph.PersonId AS {nameof(Phone.PersonId)}
		     FROM {TableName} p
		     LEFT JOIN Phone ph ON p.Id = ph.PersonId AND ph.Active = 1
		     WHERE 1 = 1
		       AND p.Id = @Id
		 """;

	internal const string GetPersonsWithPhones =
		$"""
		 	SELECT p.Id AS {nameof(Person.Id)}
		 			, p.Name AS {nameof(Person.Name)}
		 			, p.Document AS {nameof(Person.Document)}
		 			, p.BirthDate AS {nameof(Person.BirthDate)}
		 			, p.Active AS {nameof(Person.Active)}
		 			, ph.Id AS {nameof(Phone.Id)}
		 			, ph.Number AS {nameof(Phone.Number)}
		 			, ph.Type AS {nameof(Phone.Type)}
		 			, ph.PersonId AS {nameof(Phone.PersonId)}
		 	FROM {TableName} p
		 	LEFT JOIN Phone ph ON p.Id = ph.PersonId AND ph.Active = 1
		 	WHERE 1 = 1
		 	  AND p.Active = 1
		 	ORDER BY p.Name
		 """;
}