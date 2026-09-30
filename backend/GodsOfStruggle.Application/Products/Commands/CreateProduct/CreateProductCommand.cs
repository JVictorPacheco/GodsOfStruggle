using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal BasePrice, Guid CategoryId) : IRequest<ProductDto>;
