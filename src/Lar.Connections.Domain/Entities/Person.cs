using Lar.Connections.Domain.Enums;

namespace Lar.Connections.Domain.Entities;

public class Person
{
	public long Id { get; set; }
	public string? Name { get; set; }
	public string? Document { get; set; }
	public DateTime BirthDate { get; set; }
	public bool Active { get; set; }

	public List<Phone> Phones { get; set; } = [];

	public static Person Create(string name, string document, DateTime birthDate, bool active)
	{
		return new Person
		{
			Name = name,
			Document = document,
			BirthDate = birthDate,
			Active = active
		};
	}

	public void Update(long id, string name, string document, DateTime birthDate, bool active)
	{
		Id = id;
		Name = name;
		Document = document;
		BirthDate = birthDate;
		Active = active;
	}

	public void AddPhone(string number, PhoneType type)
	{
		var phone = Phone.Create(Id, number, type);
		Phones.Add(phone);
	}

	public void RemovePhone(long id)
	{
		var phone = Phones.FirstOrDefault(x => x.Id == id);
		if (phone != null) Phones.Remove(phone);
	}

	public void UpdatePhone(long id, string number, PhoneType type)
	{
		var phone = Phones.FirstOrDefault(x => x.Id == id);
		phone?.Update(Id, id, number, type);
	}

	public void ClearPhones()
	{
		Phones.Clear();
	}

	public void SetPhones(List<Phone> phones)
	{
		ClearPhones();
		Phones = phones;
	}
}