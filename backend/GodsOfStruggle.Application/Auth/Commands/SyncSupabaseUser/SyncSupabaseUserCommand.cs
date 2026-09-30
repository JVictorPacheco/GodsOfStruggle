using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Auth.Commands.SyncSupabaseUser;

public record SyncSupabaseUserCommand(Guid UserId, string Email, string Name, string? Phone) : IRequest<UserDto>;
