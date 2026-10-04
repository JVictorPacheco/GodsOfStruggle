using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal BasePrice, Guid CategoryId) : IRequest<Guid>;
