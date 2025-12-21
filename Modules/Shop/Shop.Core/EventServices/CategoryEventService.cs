using Shared.Core.Bases;
using Shop.Core.Dtos.Category;
using Shop.Infrastructure.Persistence.Entities.Categories;
using Shop.Infrastructure.Persistence.Repositories;

namespace Shop.Core.EventServices;

public interface ICategoryEventService
{
    Task CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

internal class CategoryEventService(ICategoryRepository categoryRepository) : ICategoryEventService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _categoryRepository.CreateOrUpdateForEventAsync(entity, cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _categoryRepository.DeleteByExternalIdAsync(externalId, cancellationToken);

    private async Task<CategoryEntity> MapToEntity(CategoryEventDto eventDto, CancellationToken cancellationToken)
    {
        var parentId = eventDto.ParentCategoryId.HasValue
            ? await _categoryRepository.GetIdByExternalIdAsync(eventDto.ParentCategoryId.Value, cancellationToken)
            : null;

        var subCategories = await _categoryRepository.GetListByExternalIdsAsync(eventDto.SubCategoryIds, cancellationToken);

        var entity = eventDto.Map();
        entity.ParentCategoryId = parentId;
        entity.SubCategories = subCategories;

        return entity;
    }
}