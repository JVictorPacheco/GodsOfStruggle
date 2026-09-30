using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.UpdateStock;

public class UpdateStockCommandHandler(IProductRepository productRepository)
    : IRequestHandler<UpdateStockCommand>
{
    public async Task Handle(UpdateStockCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new KeyNotFoundException($"Produto {request.ProductId} não encontrado.");

        ProductVariant variant = product.Variants.FirstOrDefault(v => v.Id == request.VariantId)
            ?? throw new KeyNotFoundException($"Variante {request.VariantId} não encontrada.");

        variant.UpdateStock(request.Quantity);
        await productRepository.UpdateAsync(product, cancellationToken);
    }
}
