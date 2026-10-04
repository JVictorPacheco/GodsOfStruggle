using GodsOfStruggle.Application.Cart.Commands.AddToCart;
using GodsOfStruggle.Application.Cart.Commands.ClearCart;
using GodsOfStruggle.Application.Cart.Commands.RemoveFromCart;
using GodsOfStruggle.Application.Cart.Queries.GetCart;
using GodsOfStruggle.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodsOfStruggle.API.Controllers;

[ApiController]
[Route("api/v1/cart")]
[Authorize]
public class CartController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<CartDto>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCart(CancellationToken cancellationToken)
    {
        CartDto cart = await mediator.Send(new GetCartQuery(), cancellationToken);
        return Ok(cart);
    }

    [HttpPost("items")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddItem([FromBody] AddToCartCommand command, CancellationToken cancellationToken)
    {
        Guid cartId = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetCart), new { }, new { cartId });
    }

    [HttpDelete("items/{itemId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> RemoveItem(Guid itemId, CancellationToken cancellationToken)
    {
        await mediator.Send(new RemoveFromCartCommand(itemId), cancellationToken);
        return NoContent();
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> ClearCart(CancellationToken cancellationToken)
    {
        await mediator.Send(new ClearCartCommand(), cancellationToken);
        return NoContent();
    }
}
