using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Auth.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<UserDto>;
