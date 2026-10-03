using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Domain.Entities;
using GodsOfStruggle.Domain.ValueObjects;
using MediatR;

namespace GodsOfStruggle.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository)
    : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = Product.Create(request.Name, request.Description, new Money(request.BasePrice), request.CategoryId);
        await productRepository.AddAsync(product, cancellationToken);
        return ProductMapper.ToDto(product);
    }
}
