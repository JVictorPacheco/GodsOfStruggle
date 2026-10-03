using GodsOfStruggle.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GodsOfStruggle.Infrastructure.Auth;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    private ClaimsPrincipal User => httpContextAccessor.HttpContext?.User
        ?? throw new InvalidOperationException("Nenhum usuário autenticado no contexto atual.");

    public Guid UserId
    {
        get
        {
            string? sub = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value;
            return Guid.TryParse(sub, out Guid id) ? id
                : throw new InvalidOperationException("UserId inválido no token.");
        }
    }

    public string Email => User.FindFirst(ClaimTypes.Email)?.Value
        ?? User.FindFirst("email")?.Value
        ?? throw new InvalidOperationException("Email não encontrado no token.");

    public bool IsAdmin =>
        User.FindFirst("role")?.Value == "admin" ||
        User.FindFirst("app_role")?.Value == "Admin";
}
