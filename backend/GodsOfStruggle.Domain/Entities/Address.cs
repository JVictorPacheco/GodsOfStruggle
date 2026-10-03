using GodsOfStruggle.Domain.Common;

namespace GodsOfStruggle.Domain.Entities;

public class Address : BaseEntity
{
    public Guid UserId { get; private set; }
    public string Label { get; private set; } = string.Empty;
    public string Street { get; private set; } = string.Empty;
    public string Number { get; private set; } = string.Empty;
    public string? Complement { get; private set; }
    public string Neighborhood { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string State { get; private set; } = string.Empty;
    public string ZipCode { get; private set; } = string.Empty;
    public bool IsDefault { get; private set; }

    private Address() { }

    public static Address Create(Guid userId, string label, string street, string number,
        string? complement, string neighborhood, string city, string state, string zipCode, bool isDefault = false)
    {
        return new Address
        {
            UserId = userId,
            Label = label,
            Street = street,
            Number = number,
            Complement = complement,
            Neighborhood = neighborhood,
            City = city,
            State = state,
            ZipCode = zipCode,
            IsDefault = isDefault
        };
    }

    public void SetAsDefault() { IsDefault = true; UpdateTimestamp(); }
    public void UnsetDefault() { IsDefault = false; UpdateTimestamp(); }
}
