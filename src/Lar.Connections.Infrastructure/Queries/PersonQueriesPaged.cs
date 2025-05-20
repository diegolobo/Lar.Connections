using Lar.Connections.Domain.Entities;

namespace Lar.Connections.Infrastructure.Queries;

internal static class PersonQueriesPaged
{
	internal const string GetPeoplePagedBase =
		$$"""

		              WITH PersonCTE AS (
		                  SELECT p.Id, p.Name, p.Document, p.BirthDate, p.Active,
		                         ROW_NUMBER() OVER ({0}) as RowNum
		                  FROM {{PersonQueries.TableName}} p
		                  WHERE 1 = 1 {1}
		              )
		              SELECT p.Id, p.Name, p.Document, p.BirthDate, p.Active
		              FROM PersonCTE p
		              WHERE p.RowNum BETWEEN @Offset + 1 AND @Offset + @PageSize
		              ORDER BY p.RowNum
		  """;

	internal const string GetPeopleWithPhonesPagedBase =
		$$"""
		              WITH PersonCTE AS (
		                  SELECT p.Id, p.Name, p.Document, p.BirthDate, p.Active,
		                         ROW_NUMBER() OVER ({0}) as RowNum
		                  FROM {{PersonQueries.TableName}} p
		                  WHERE 1 = 1 {1}
		              )
		              SELECT p.Id, p.Name, p.Document, p.BirthDate, p.Active,
		                     ph.Id, ph.Number, ph.Type, ph.PersonId
		              FROM PersonCTE p
		              LEFT JOIN {{PhoneQueries.TableName}} ph ON p.Id = ph.PersonId
		              WHERE p.RowNum BETWEEN @Offset + 1 AND @Offset + @PageSize
		              ORDER BY p.RowNum, ph.Id
		  """;

	internal const string GetPeopleCountBase =
		$$"""
		  	SELECT COUNT(*)
		  	FROM {{PersonQueries.TableName}} p
		  	WHERE 1 = 1 
		  		{0}
		  """;

	internal static string BuildWhereClause(
		string? searchTerm,
		bool includeInactive,
		long? id = null,
		string? document = null)
	{
		var conditions = new List<string>();

		if (!includeInactive) conditions.Add("AND p.Active = 1");

		if (id.HasValue) conditions.Add("AND p.Id = @Id");

		if (!string.IsNullOrWhiteSpace(document)) conditions.Add("AND p.Document = @Document");

		if (!string.IsNullOrWhiteSpace(searchTerm))
			conditions.Add("AND (p.Name LIKE @SearchTerm OR p.Document LIKE @SearchTerm)");

		return string.Join(" ", conditions);
	}

	internal static string BuildOrderByClause(string? sortBy, bool sortDescending)
	{
		var orderDirection = sortDescending ? "DESC" : "ASC";

		var sortField = sortBy switch
		{
			nameof(Person.Name) => "p.Name",
			nameof(Person.Document) => "p.Document",
			nameof(Person.BirthDate) => "p.BirthDate",
			nameof(Person.Id) => "p.Id",
			_ => "p.Name"
		};

		return $"ORDER BY {sortField} {orderDirection}";
	}
}