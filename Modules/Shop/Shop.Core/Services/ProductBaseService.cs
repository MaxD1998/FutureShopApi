using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Infrastructure.Extensions;
using Shared.Shared.Dtos;
using Shop.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.Services;

public interface IProductBaseService
{
    Task<ResultDto<ProductBaseResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNametByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseResponseFormDto>> UpdateAsync(Guid id, ProductBaseRequestFormDto dto, CancellationToken cancellationToken);
}

internal class ProductBaseService(IProductBaseRepository productBaseRepository) : IProductBaseService
{
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;

    public async Task<ResultDto<ProductBaseResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productBaseRepository.GetByIdAsync(id, ProductBaseResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<IdNameDto>> GetIdNametByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productBaseRepository.GetByIdAsync(id, IdNameDto.MapFromProductBase(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetPageAsync(pagination, ProductBaseListDto.Map(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<ProductBaseResponseFormDto>> UpdateAsync(Guid id, ProductBaseRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productBaseRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);
        var result = await _productBaseRepository.GetByIdAsync(entity.Id, ProductBaseResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}