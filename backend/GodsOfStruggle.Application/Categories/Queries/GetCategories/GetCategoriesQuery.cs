using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Categories.Queries.GetCategories;

public record GetCategoriesQuery : IRequest<List<CategoryDto>>;
