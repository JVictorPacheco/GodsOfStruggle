using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Application.Products.Commands.CreateProduct;
using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.ValueObjects;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<UpdateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Produto {request.Id} não encontrado.");

        product.Update(request.Name, request.Description, new Money(request.BasePrice), request.CategoryId);
        await productRepository.UpdateAsync(product, cancellationToken);
        return ProductMapper.ToDto(product);
    }
}
