using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Auth.Commands.SyncSupabaseUser;

public class SyncSupabaseUserCommandHandler(IUserRepository userRepository)
    : IRequestHandler<SyncSupabaseUserCommand, UserDto>
{
    public async Task<UserDto> Handle(SyncSupabaseUserCommand request, CancellationToken cancellationToken)
    {
        User? existing = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

        if (existing is null)
        {
            User newUser = User.Create(request.UserId, request.Email, request.Name, request.Phone);
            await userRepository.AddAsync(newUser, cancellationToken);
            return ToDto(newUser);
        }

        existing.UpdateProfile(request.Name, request.Phone);
        await userRepository.UpdateAsync(existing, cancellationToken);
        return ToDto(existing);
    }

    private static UserDto ToDto(User user) =>
        new(user.Id, user.Email, user.Name, user.Phone, user.Role, user.CreatedAt);
}
