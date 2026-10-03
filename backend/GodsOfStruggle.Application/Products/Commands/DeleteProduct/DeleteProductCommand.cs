using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;
