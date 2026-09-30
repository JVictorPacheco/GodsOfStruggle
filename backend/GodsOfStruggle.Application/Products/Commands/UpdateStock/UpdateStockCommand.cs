using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.UpdateStock;

public record UpdateStockCommand(Guid ProductId, Guid VariantId, int Quantity) : IRequest;
