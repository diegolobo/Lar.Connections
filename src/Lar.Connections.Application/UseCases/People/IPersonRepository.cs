using Lar.Connections.Domain.Entities;

namespace Lar.Connections.Application.UseCases.People;

public interface IPersonRepository
{
	Task<Person?> GetByIdAsync(long id);
	Task<Person?> GetByDocumentAsync(string document);
	Task<List<Phone>> GetPhonesByPersonIdAsync(long personId);
	Task<Person?> GetPersonWithPhonesAsync(long id);
	Task<long> CreateAsync(Person person);
	Task<bool> UpdateAsync(Person person);
	Task<bool> DeleteAsync(long id);
	Task<bool> ActivateAsync(long id);
	Task<bool> DeactivateAsync(long id);
	Task<bool> AddPhoneAsync(long personId, Phone phone);
	Task<bool> UpdatePhoneAsync(long personId, Phone phone);
	Task<bool> DeletePhoneAsync(long id);

	Task<(List<Person> People, int TotalCount)> GetPeoplePagedAsync(
		int page,
		int pageSize,
		string? searchTerm = null,
		bool includeInactive = false,
		bool includePhones = true,
		string? sortBy = nameof(Person.Name),
		bool sortDescending = false);
}