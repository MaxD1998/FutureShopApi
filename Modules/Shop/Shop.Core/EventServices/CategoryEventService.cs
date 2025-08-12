using Shared.Core.Bases;
using Shop.Core.Dtos.Category;
using Shop.Domain.Aggregates.Categories;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.EventServices;

public interface ICategoryEventService
{
    Task CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

public class CategoryEventService(ICategoryRepository categoryRepository) : BaseService, ICategoryEventService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _categoryRepository.CreateOrUpdateForEventAsync(entity, cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _categoryRepository.DeleteByExternalIdAsync(externalId, cancellationToken);

    private async Task<CategoryAggregate> MapToEntity(CategoryEventDto eventDto, CancellationToken cancellationToken)
    {
        var parentId = eventDto.ParentCategoryId.HasValue
            ? await _categoryRepository.GetIdByExternalIdAsync(eventDto.ParentCategoryId.Value, cancellationToken)
            : null;

        var subCategories = await _categoryRepository.GetListByExternalIdsAsync(eventDto.SubCategoryIds, cancellationToken);
        var entity = new CategoryAggregate(eventDto.Id, eventDto.Name, parentId, subCategories);

        return entity;
    }
}