using GodsOfStruggle.Application.Common;
using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Application.Products.Commands.CreateProduct;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Products.Queries.GetProductById;

public class GetProductByIdQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product? product = await productRepository.GetByIdAsync(request.Id, cancellationToken);
        if (product is null)
            return Result<ProductDto>.Failure("Produto não encontrado.");

        Category? category = await categoryRepository.GetByIdAsync(product.CategoryId, cancellationToken);
        return Result<ProductDto>.Success(ProductMapper.ToDto(product, category?.Name));
    }
}
