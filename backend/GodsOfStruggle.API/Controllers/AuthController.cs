using GodsOfStruggle.Application.Auth.Commands.SyncSupabaseUser;
using GodsOfStruggle.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GodsOfStruggle.API.Controllers;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(ISender mediator) : ControllerBase
{
    [HttpPost("sync")]
    [Authorize]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Sync(CancellationToken cancellationToken)
    {
        string? sub = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
        string? email = User.FindFirstValue(ClaimTypes.Email) ?? User.FindFirstValue("email");
        string? name = User.FindFirstValue("name") ?? User.FindFirstValue(ClaimTypes.Name) ?? email;

        if (!Guid.TryParse(sub, out Guid userId) || email is null)
            return Unauthorized();

        UserDto result = await mediator.Send(
            new SyncSupabaseUserCommand(userId, email, name!, null),
            cancellationToken);

        return Ok(result);
    }
}
