using Dapper;

using Lar.Connections.Application.UseCases.People;
using Lar.Connections.Domain.Entities;
using Lar.Connections.Infrastructure.Constants;
using Lar.Connections.Infrastructure.Queries;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Lar.Connections.Infrastructure.Repositories;

internal class PersonRepository : IPersonRepository
{
	private readonly string _connectionString;

	public PersonRepository(IConfiguration configuration)
	{
		var connectionString = configuration.GetConnectionString(DatabaseConstants.DefaultConnectionName);

		if (string.IsNullOrEmpty(connectionString))
			throw new Exception(DatabaseConstants.EmptyConnectionStringErrorMessage);

		_connectionString = connectionString;
	}

	public async Task<Person?> GetByIdAsync(long id)
	{
		await using var connection = new SqlConnection(_connectionString);
		var person = await connection.QueryFirstOrDefaultAsync<Person>(
			PersonQueries.GetById,
			new { Id = id });

		return person;
	}

	public async Task<List<Person>> GetAllAsync()
	{
		await using var connection = new SqlConnection(_connectionString);
		var persons = await connection.QueryAsync<Person>(PersonQueries.GetAll);
		return persons.ToList();
	}

	public async Task<List<Person>> GetActiveAsync()
	{
		await using var connection = new SqlConnection(_connectionString);
		var persons = await connection.QueryAsync<Person>(PersonQueries.GetActive);
		return persons.ToList();
	}

	public async Task<Person?> GetByDocumentAsync(string document)
	{
		await using var connection = new SqlConnection(_connectionString);
		var person = await connection.QueryFirstOrDefaultAsync<Person>(
			PersonQueries.GetByDocument,
			new { Document = document });

		return person;
	}

	public async Task<long> CreateAsync(Person person)
	{
		await using var connection = new SqlConnection(_connectionString);
		var id = await connection.QuerySingleAsync<long>(
			PersonQueries.Insert,
			new
			{
				person.Name,
				person.Document,
				person.BirthDate,
				person.Active
			});

		return id;
	}

	public async Task<bool> UpdateAsync(Person person)
	{
		await using var connection = new SqlConnection(_connectionString);
		var rowsAffected = await connection.ExecuteAsync(
			PersonQueries.Update,
			new
			{
				person.Id,
				person.Name,
				person.Document,
				person.BirthDate,
				person.Active
			});

		return rowsAffected > 0;
	}

	public async Task<bool> DeleteAsync(long id)
	{
		await using var connection = new SqlConnection(_connectionString);

		await connection.ExecuteAsync(
			PhoneQueries.DeleteByPersonId,
			new { PersonId = id });

		var rowsAffected = await connection.ExecuteAsync(
			PersonQueries.Delete,
			new { Id = id });

		return rowsAffected > 0;
	}

	public async Task<bool> ActivateAsync(long id)
	{
		await using var connection = new SqlConnection(_connectionString);
		var rowsAffected = await connection.ExecuteAsync(
			PersonQueries.Activate,
			new { Id = id });

		return rowsAffected > 0;
	}

	public async Task<bool> DeactivateAsync(long id)
	{
		await using var connection = new SqlConnection(_connectionString);
		var rowsAffected = await connection.ExecuteAsync(
			PersonQueries.Deactivate,
			new { Id = id });

		return rowsAffected > 0;
	}

	public async Task<bool> AddPhoneAsync(long personId, Phone phone)
	{
		await using var connection = new SqlConnection(_connectionString);
		var id = await connection.QuerySingleAsync<long>(
			PhoneQueries.Insert,
			new
			{
				phone.Number,
				phone.Type,
				PersonId = personId
			});

		return id > 0;
	}

	public async Task<bool> UpdatePhoneAsync(long personId, Phone phone)
	{
		await using var connection = new SqlConnection(_connectionString);
		var rowsAffected = await connection.ExecuteAsync(
			PhoneQueries.Update,
			new
			{
				phone.Id,
				phone.Number,
				phone.Type,
				PersonId = personId
			});

		return rowsAffected > 0;
	}

	public async Task<bool> RemovePhoneAsync(long personId, long phoneId)
	{
		await using var connection = new SqlConnection(_connectionString);
		var rowsAffected = await connection.ExecuteAsync(
			PhoneQueries.Remove,
			new
			{
				Id = phoneId,
				PersonId = personId
			});

		return rowsAffected > 0;
	}

	public async Task<List<Phone>> GetPhonesByPersonIdAsync(long personId)
	{
		await using var connection = new SqlConnection(_connectionString);
		var phones = await connection.QueryAsync<Phone>(
			PhoneQueries.GetByPersonId,
			new { PersonId = personId });

		return phones.ToList();
	}

	public async Task<Person?> GetPersonWithPhonesAsync(long id)
	{
		await using var connection = new SqlConnection(_connectionString);

		var personDict = new Dictionary<long, Person>();

		var result = await connection.QueryAsync<Person, Phone, Person>(
			PersonQueries.GetPersonWithPhones,
			(person, phone) =>
			{
				if (!personDict.TryGetValue(person.Id, out var existingPerson))
				{
					existingPerson = person;
					existingPerson.Phones = [];
					personDict.Add(person.Id, existingPerson);
				}

				if (phone.Id > 0) existingPerson.Phones.Add(phone);

				return existingPerson;
			},
			new { Id = id },
			splitOn: nameof(Phone.Id));

		return personDict.Values.FirstOrDefault();
	}

	public async Task<List<Person>> GetPersonsWithPhonesAsync()
	{
		await using var connection = new SqlConnection(_connectionString);

		var personDict = new Dictionary<long, Person>();

		await connection.QueryAsync<Person, Phone, Person>(
			PersonQueries.GetPersonsWithPhones,
			(person, phone) =>
			{
				if (!personDict.TryGetValue(person.Id, out var existingPerson))
				{
					existingPerson = person;
					existingPerson.Phones = [];
					personDict.Add(person.Id, existingPerson);
				}

				if (phone.Id > 0) existingPerson.Phones.Add(phone);

				return existingPerson;
			},
			splitOn: nameof(Phone.Id));

		return personDict.Values.ToList();
	}
}