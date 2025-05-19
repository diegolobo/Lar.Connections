using Lar.Connections.Domain.Enums;

namespace Lar.Connections.Domain.Entities;

public class Phone
{
	public long Id { get; set; }
	public string? Number { get; set; }
	public PhoneType? Type { get; set; }
	public bool Active { get; set; }
	public long PersonId { get; set; }
	public Person? Person { get; set; }

	public static Phone Create(long personId, string number, PhoneType type)
	{
		return new Phone
		{
			PersonId = personId,
			Number = number,
			Type = type,
			Active = true
		};
	}

	public void Update(long personId, long id, string number, PhoneType type)
	{
		PersonId = personId;
		Id = id;
		Number = number;
		Type = type;
	}
}