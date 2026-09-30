namespace GodsOfStruggle.Domain.ValueObjects;

public sealed record ShippingAddress(
    string Street,
    string Number,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string ZipCode);
