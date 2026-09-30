using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Products.Queries.GetProducts;

public record GetProductsQuery(string? Search, Guid? CategoryId, decimal? MinPrice, decimal? MaxPrice, int Page = 1, int PageSize = 20) : IRequest<PagedResult<ProductListItemDto>>;
