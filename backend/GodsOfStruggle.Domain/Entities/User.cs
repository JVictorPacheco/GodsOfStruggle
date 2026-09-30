using GodsOfStruggle.Domain.Common;
using GodsOfStruggle.Domain.Enums;

namespace GodsOfStruggle.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Phone { get; private set; }
    public UserRole Role { get; private set; } = UserRole.Customer;

    public IReadOnlyCollection<Address> Addresses => _addresses.AsReadOnly();
    private readonly List<Address> _addresses = [];

    private User() { }

    public static User Create(Guid supabaseUid, string email, string name, string? phone = null, UserRole role = UserRole.Customer)
    {
        return new User
        {
            Id = supabaseUid,
            Email = email,
            Name = name,
            Phone = phone,
            Role = role
        };
    }

    public void UpdateProfile(string name, string? phone)
    {
        Name = name;
        Phone = phone;
        UpdateTimestamp();
    }

    public void AddAddress(Address address) { _addresses.Add(address); UpdateTimestamp(); }

    public void PromoteToAdmin() { Role = UserRole.Admin; UpdateTimestamp(); }
}
