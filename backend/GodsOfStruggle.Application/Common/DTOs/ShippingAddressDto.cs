namespace GodsOfStruggle.Application.Common.DTOs;

public record ShippingAddressDto(
    string Street,
    string Number,
    string? Complement,
    string Neighborhood,
    string City,
    string State,
    string ZipCode);
