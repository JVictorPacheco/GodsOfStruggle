using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler(IUserRepository userRepository, ICurrentUserService currentUser)
    : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(currentUser.UserId, cancellationToken);

        if (user is null)
            throw new InvalidOperationException("Usuário autenticado não encontrado na base de dados.");

        return new UserDto(user.Id, user.Email, user.Name, user.Phone, user.Role, user.CreatedAt);
    }
}
