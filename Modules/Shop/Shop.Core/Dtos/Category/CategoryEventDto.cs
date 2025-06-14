﻿using Shop.Domain.Entities;

namespace Shop.Core.Dtos.Category;

public class CategoryEventDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public List<Guid> SubCategoryIds { get; set; } = [];

    public CategoryEntity Map() => new()
    {
        ExternalId = Id,
        Name = Name,
    };
}