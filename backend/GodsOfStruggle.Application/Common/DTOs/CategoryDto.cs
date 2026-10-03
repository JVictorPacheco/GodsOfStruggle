namespace GodsOfStruggle.Application.Common.DTOs;

public record CategoryDto(
    Guid Id,
    string Name,
    string Description,
    string Slug,
    Guid? ParentCategoryId,
    bool IsActive);
