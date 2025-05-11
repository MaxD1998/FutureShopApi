using Shared.Core.Bases;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.EventServices;

public interface ICategoryEventService
{
    Task CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken);

    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}

public class CategoryEventService(ICategoryRepository categoryRepository) : BaseService, ICategoryEventService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;

    public async Task CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _categoryRepository.CreateOrUpdateForEventAsync(entity, cancellationToken); ;
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
        => _categoryRepository.DeleteByIdAsync(id, cancellationToken);

    private async Task<CategoryEntity> MapToEntity(CategoryEventDto eventDto, CancellationToken cancellationToken)
    {
        var parentIdTask = eventDto.ParentCategoryId.HasValue
            ? _categoryRepository.GetIdByExternalIdAsync(eventDto.ParentCategoryId.Value, cancellationToken)
            : null;

        var subCategoriesTask = _categoryRepository.GetListByExternalIdsAsync(eventDto.SubCategoryIds, cancellationToken);

        await Task.WhenAll(parentIdTask, subCategoriesTask);

        var entity = eventDto.Map();
        entity.ParentCategoryId = parentIdTask.Result;
        entity.SubCategories = subCategoriesTask.Result;

        return entity;
    }
}