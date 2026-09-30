using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal BasePrice, Guid CategoryId) : IRequest<ProductDto>;
