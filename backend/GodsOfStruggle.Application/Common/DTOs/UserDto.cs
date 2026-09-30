using GodsOfStruggle.Domain.Enums;

namespace GodsOfStruggle.Application.Common.DTOs;

public record UserDto(
    Guid Id,
    string Email,
    string Name,
    string? Phone,
    UserRole Role,
    DateTime CreatedAt);
