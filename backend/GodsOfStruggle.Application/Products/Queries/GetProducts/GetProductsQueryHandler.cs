using GodsOfStruggle.Application.Common.DTOs;
using GodsOfStruggle.Application.Interfaces;
using GodsOfStruggle.Application.Products.Commands.CreateProduct;
using GodsOfStruggle.Domain.Entities;
using MediatR;

namespace GodsOfStruggle.Application.Products.Queries.GetProducts;

public class GetProductsQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository)
    : IRequestHandler<GetProductsQuery, PagedResult<ProductListItemDto>>
{
    public async Task<PagedResult<ProductListItemDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        PagedResult<Product> paged = await productRepository.GetPagedAsync(
            request.Search, request.CategoryId, request.MinPrice, request.MaxPrice,
            request.Page, request.PageSize, cancellationToken);

        List<Category> categories = await categoryRepository.GetAllActiveAsync(cancellationToken);
        Dictionary<Guid, string> categoryNames = categories.ToDictionary(c => c.Id, c => c.Name);

        List<ProductListItemDto> items = paged.Items
            .Select(p => ProductMapper.ToListItemDto(p, categoryNames.GetValueOrDefault(p.CategoryId)))
            .ToList();

        return new PagedResult<ProductListItemDto>(items, paged.TotalCount, paged.Page, paged.PageSize);
    }
}
