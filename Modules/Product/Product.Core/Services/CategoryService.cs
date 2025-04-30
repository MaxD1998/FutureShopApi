using Product.Core.Dtos;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Product.Infrastructure.Repositories;
using Shared.Core.Bases;
using Shared.Core.Dtos;
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

public class CategoryService(ICategoryRepository categoryRepository, ProductPostgreSqlContext context) : BaseService, ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly ProductPostgreSqlContext _context = context;

    public async Task<ResultDto<CategoryFormDto>> CreateAsync(CategoryFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.CreateAsync(dto.ToEntity(_context), cancellationToken);
        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteByIdAsync<CategoryEntity>(id, cancellationToken);
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
        var entity = await _categoryRepository.UpdateAsync(id, dto.ToEntity(_context), cancellationToken);
        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryFormDto.Map(), cancellationToken);

        return Success(result);
    }
}