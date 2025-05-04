using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface ICategoryService
{
    Task<ResultDto> CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<CategoryListDto>>> GetActiveListAsync(CancellationToken cancellationToken);

    Task<ResultDto<CategoryFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<CategoryPageListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken);

    Task<ResultDto<CategoryFormDto>> UdpateAsync(Guid id, CategoryFormDto dto, CancellationToken cancellationToken);
}

public class CategoryService(ICategoryRepository categoryRepository, IHeaderService headerService) : BaseService, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IHeaderService _headerService = headerService;

    public async Task<ResultDto> CreateOrUpdateAsync(CategoryEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _categoryRepository.CreateOrUpdateForEventAsync(entity, cancellationToken);

        return Success();
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteByIdAsync(id, cancellationToken);

        return Success();
    }

    public async Task<ResultDto<List<CategoryListDto>>> GetActiveListAsync(CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListAsync(x => x.IsActive, CategoryListDto.Map(_headerService.GetHeader(HeaderNameConst.Lang)), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<CategoryFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetByIdAsync(id, CategoryFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetByIdAsync(id, IdNameDto.MapFromCategory(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<PageDto<CategoryPageListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetPageAsync(pageNumber, CategoryPageListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<CategoryFormDto>> UdpateAsync(Guid id, CategoryFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryFormDto.Map(), cancellationToken);

        return Success(result);
    }

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