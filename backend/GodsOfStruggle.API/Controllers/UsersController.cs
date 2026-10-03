using GodsOfStruggle.Application.Auth.Queries.GetCurrentUser;
using GodsOfStruggle.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodsOfStruggle.API.Controllers;

[ApiController]
[Route("api/v1/users")]
[Authorize]
public class UsersController(ISender mediator) : ControllerBase
{
    [HttpGet("me")]
    [ProducesResponseType<UserDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetMe(CancellationToken cancellationToken)
    {
        UserDto result = await mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        return Ok(result);
    }
}
