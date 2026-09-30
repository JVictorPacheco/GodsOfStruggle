using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Application.Products.Commands.CreateProduct;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    : IRequestHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product product = await productRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Produto {request.Id} não encontrado.");

        Category? category = await categoryRepository.GetByIdAsync(product.CategoryId, cancellationToken);
        return ProductMapper.ToDto(product, category?.Name);
    }
}
