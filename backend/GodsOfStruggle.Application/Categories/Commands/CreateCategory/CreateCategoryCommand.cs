using GodsOfStruggle.Application.Common.DTOs;
using MediatR;

namespace GodsOfStruggle.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand(string Name, string Description, string Slug, Guid? ParentCategoryId) : IRequest<CategoryDto>;
