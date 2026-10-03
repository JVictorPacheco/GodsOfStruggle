using GodsOfStruggle.Application.Categories.Commands.CreateCategory;
using GodsOfStruggle.Application.Categories.Queries.GetCategories;
using GodsOfStruggle.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodsOfStruggle.API.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoriesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<List<CategoryDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        List<CategoryDto> result = await mediator.Send(new GetCategoriesQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType<CategoryDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        CategoryDto result = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
}
