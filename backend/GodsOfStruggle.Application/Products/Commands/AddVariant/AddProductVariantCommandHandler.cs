using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.ValueObjects;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.AddVariant;

public class AddProductVariantCommandHandler(IProductRepository productRepository)
    : IRequestHandler<AddProductVariantCommand, ProductVariantDto>
{
    public async Task<ProductVariantDto> Handle(AddProductVariantCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken)
            ?? throw new KeyNotFoundException($"Produto {request.ProductId} não encontrado.");

        ProductVariant variant = product.AddVariant(request.SKU, request.Attributes, new Money(request.Price), request.StockQuantity);
        await productRepository.UpdateAsync(product, cancellationToken);
        return new ProductVariantDto(variant.Id, variant.SKU, variant.Attributes, variant.Price.Amount, variant.Price.Currency, variant.StockQuantity, variant.IsActive);
    }
}
