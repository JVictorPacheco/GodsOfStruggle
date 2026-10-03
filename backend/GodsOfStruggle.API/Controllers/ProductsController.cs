using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Products.Commands.AddVariant;
using GodsOfStruggle.Application.Products.Commands.CreateProduct;
using GodsOfStruggle.Application.Products.Commands.DeleteProduct;
using GodsOfStruggle.Application.Products.Commands.UpdateProduct;
using GodsOfStruggle.Application.Products.Commands.UpdateStock;
using GodsOfStruggle.Application.Products.Queries.GetProductById;
using GodsOfStruggle.Application.Products.Queries.GetProducts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GodsOfStruggle.API.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType<PagedResult<ProductListItemDto>>(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromQuery] string? search,
        [FromQuery] Guid? categoryId,
        [FromQuery] decimal? minPrice,
        [FromQuery] decimal? maxPrice,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        CancellationToken cancellationToken = default)
    {
        PagedResult<ProductListItemDto> result = await mediator.Send(
            new GetProductsQuery(search, categoryId, minPrice, maxPrice, page, pageSize),
            cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            ProductDto result = await mediator.Send(new GetProductByIdQuery(id), cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType<ProductDto>(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand command, CancellationToken cancellationToken)
    {
        ProductDto result = await mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [ProducesResponseType<ProductDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductCommand command, CancellationToken cancellationToken)
    {
        try
        {
            ProductDto result = await mediator.Send(command with { Id = id }, cancellationToken);
            return Ok(result);
        }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await mediator.Send(new DeleteProductCommand(id), cancellationToken);
            return NoContent();
        }
        catch (KeyNotFoundException) { return NotFound(); }
    }

    [HttpPost("{id:guid}/variants")]
    [Authorize]
    [ProducesResponseType<ProductVariantDto>(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddVariant(Guid id, [FromBody] AddProductVariantCommand command, CancellationToken cancellationToken)
    {
        ProductVariantDto result = await mediator.Send(command with { ProductId = id }, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id }, result);
    }

    [HttpPut("{id:guid}/variants/{variantId:guid}/stock")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateStock(Guid id, Guid variantId, [FromBody] int quantity, CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateStockCommand(id, variantId, quantity), cancellationToken);
        return NoContent();
    }
}
