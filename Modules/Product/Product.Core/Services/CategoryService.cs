using Product.Core.Dtos;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure.Repositories;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;

namespace Product.Core.Services;

public interface ICategoryService
{
    Task<ResultDto<CategoryFormDto>> CreateAsync(CategoryFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<CategoryFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListPotentialParentCategories(Guid? id, List<Guid> childIds, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListPotentialSubcategoriesAsync(Guid? id, Guid? parentId, List<Guid> childIds, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<CategoryListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken);

    Task<ResultDto<CategoryFormDto>> UpdateAsync(Guid id, CategoryFormDto dto, CancellationToken cancellationToken);
}

public class CategoryService(ICategoryRepository categoryRepository, IRabbitMqContext rabbitMqContext) : BaseService, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;

    public async Task<ResultDto<CategoryFormDto>> CreateAsync(CategoryFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        entity = await _categoryRepository.CreateAsync(entity, cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteByIdAsync(id, cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(id, MessageType.Delete));

        return Success();
    }

    public async Task<ResultDto<CategoryFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetByIdAsync(id, CategoryFormDto.Map(), cancellationToken);
        return Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListAsync(IdNameDto.MapFromCategory(), cancellationToken);
        return Success(results);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListPotentialParentCategories(Guid? id, List<Guid> childIds, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListPotentialParentCategories(id, childIds, IdNameDto.MapFromCategory(), cancellationToken);
        return Success(results);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListPotentialSubcategoriesAsync(Guid? id, Guid? parentId, List<Guid> childIds, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListPotentialSubcategoriesAsync(id, parentId, childIds, IdNameDto.MapFromCategory(), cancellationToken);
        return Success(results);
    }

    public async Task<ResultDto<PageDto<CategoryListDto>>> GetPageListAsync(int pageNumber, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetPageAsync(pageNumber, CategoryListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<CategoryFormDto>> UpdateAsync(Guid id, CategoryFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        entity = await _categoryRepository.UpdateAsync(id, entity, cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryFormDto.Map(), cancellationToken);

        return Success(result);
    }

    private async Task<CategoryEntity> MapToEntity(CategoryFormDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEntity();
        var subcategoryIds = dto.SubCategories.Select(x => x.Id).ToList();

        entity.SubCategories = await _categoryRepository.GetListByIds(subcategoryIds, cancellationToken);

        return entity;
    }
}