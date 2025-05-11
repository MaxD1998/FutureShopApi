using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Infrastructure.Extensions;
using Shop.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IProductBaseService
{
    Task<ResultDto<ProductBaseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNametByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageAsync(int pageIndex, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseFormDto>> UpdateAsync(Guid id, ProductBaseFormDto dto, CancellationToken cancellationToken);
}

public class ProductBaseService(IProductBaseRepository productBaseRepository) : BaseService, IProductBaseService
{
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;

    public async Task<ResultDto<ProductBaseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productBaseRepository.GetByIdAsync(id, ProductBaseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<IdNameDto>> GetIdNametByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productBaseRepository.GetByIdAsync(id, IdNameDto.MapFromProductBase(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageAsync(int pageIndex, CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetPageAsync(pageIndex, ProductBaseListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<ProductBaseFormDto>> UpdateAsync(Guid id, ProductBaseFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productBaseRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _productBaseRepository.GetByIdAsync(entity.Id, ProductBaseFormDto.Map(), cancellationToken);

        return Success(result);
    }
}