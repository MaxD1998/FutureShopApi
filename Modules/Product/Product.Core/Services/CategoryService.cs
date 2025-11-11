using Product.Core.Dtos;
using Product.Core.Dtos.Category;
using Product.Infrastructure.Entities;
using Product.Infrastructure.Repositories;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;

namespace Product.Core.Services;

public interface ICategoryService
{
    Task<ResultDto<CategoryResponseFormDto>> CreateAsync(CategoryRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<CategoryResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListPotentialParentCategories(Guid? id, List<Guid> childIds, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListPotentialSubcategoriesAsync(Guid? id, Guid? parentId, List<Guid> childIds, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<CategoryListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<CategoryResponseFormDto>> UpdateAsync(Guid id, CategoryRequestFormDto dto, CancellationToken cancellationToken);
}

internal class CategoryService(ICategoryRepository categoryRepository, IRabbitMqContext rabbitMqContext) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;

    public async Task<ResultDto<CategoryResponseFormDto>> CreateAsync(CategoryRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        entity = await _categoryRepository.CreateAsync(entity, cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(new CategoryEventDto(entity), MessageType.AddOrUpdate));

        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteByIdAsync(id, cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(id, MessageType.Delete));

        return ResultDto.Success();
    }

    public async Task<ResultDto<CategoryResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetByIdAsync(id, CategoryResponseFormDto.Map(), cancellationToken);
        return ResultDto.Success(result);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListAsync(IdNameDto.MapFromCategory(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListPotentialParentCategories(Guid? id, List<Guid> childIds, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListPotentialParentCategories(id, childIds, IdNameDto.MapFromCategory(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListPotentialSubcategoriesAsync(Guid? id, Guid? parentId, List<Guid> childIds, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListPotentialSubcategoriesAsync(id, parentId, childIds, IdNameDto.MapFromCategory(), cancellationToken);
        return ResultDto.Success(results);
    }

    public async Task<ResultDto<PageDto<CategoryListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetPageAsync(pagination, CategoryListDto.Map(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<CategoryResponseFormDto>> UpdateAsync(Guid id, CategoryRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        entity = await _categoryRepository.UpdateAsync(id, entity, cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleCategory, EventMessageDto.Create(new CategoryEventDto(entity), MessageType.AddOrUpdate));

        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    private async Task<CategoryEntity> MapToEntity(CategoryRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ToEntity();
        var subcategoryIds = dto.SubCategories.Select(x => x.Id).ToList();

        entity.SubCategories = await _categoryRepository.GetListByIds(subcategoryIds, cancellationToken);

        return entity;
    }
}