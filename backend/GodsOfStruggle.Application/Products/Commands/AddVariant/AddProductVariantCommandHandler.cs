using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.Exceptions;
using GodsOfStruggle.Domain.ValueObjects;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.AddVariant;

public class AddProductVariantCommandHandler(IProductRepository productRepository)
    : IRequestHandler<AddProductVariantCommand, Guid>
{
    public async Task<Guid> Handle(AddProductVariantCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new NotFoundException($"Produto {request.ProductId} não encontrado.");

        ProductVariant variant = product.AddVariant(request.SKU, request.Attributes, new Money(request.Price), request.StockQuantity);
        await productRepository.UpdateAsync(product, cancellationToken);
        return variant.Id;
    }
}
