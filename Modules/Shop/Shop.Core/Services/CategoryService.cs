using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.Category;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface ICategoryService
{
    Task<ResultDto<IdNameDto>> GetActiveIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<CategoryListDto>>> GetActiveListAsync(CancellationToken cancellationToken);

    Task<ResultDto<CategoryResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<CategoryPageListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<CategoryResponseFormDto>> UdpateAsync(Guid id, CategoryRequestFormDto dto, CancellationToken cancellationToken);
}

internal class CategoryService(ICategoryRepository categoryRepository, IHeaderService headerService) : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IHeaderService _headerService = headerService;

    public async Task<ResultDto<IdNameDto>> GetActiveIdNameByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetActiveIdByIdAsync(id, IdNameDto.MapFromCategory(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<List<CategoryListDto>>> GetActiveListAsync(CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetListAsync(x => x.IsActive, CategoryListDto.Map(_headerService.GetHeader(HeaderNameConst.Lang)), cancellationToken);
        var tree = CategoryListDto.BuildTree(results);

        return ResultDto.Success(tree);
    }

    public async Task<ResultDto<CategoryResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetByIdAsync(id, CategoryResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetByIdAsync(id, IdNameDto.MapFromCategory(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<PageDto<CategoryPageListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _categoryRepository.GetPageAsync(pagination, CategoryPageListDto.Map(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<CategoryResponseFormDto>> UdpateAsync(Guid id, CategoryRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _categoryRepository.GetByIdAsync(entity.Id, CategoryResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}