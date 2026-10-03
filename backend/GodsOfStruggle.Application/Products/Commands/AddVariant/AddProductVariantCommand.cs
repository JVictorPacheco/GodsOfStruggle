using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.AddVariant;

public record AddProductVariantCommand(Guid ProductId, string SKU, Dictionary<string, string> Attributes, decimal Price, int StockQuantity) : IRequest<Guid>;
