using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.ProductBase;
using Shop.Domain.Entities;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.EventServices;

public interface IProductBaseEventService
{
    Task<ResultDto> CreateOrUpdateAsync(ProductBaseEventDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}

public class ProductBaseEventService(ICategoryRepository categoryRepository, IProductBaseRepository productBaseRepository) : BaseService, IProductBaseEventService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;

    public async Task<ResultDto> CreateOrUpdateAsync(ProductBaseEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _productBaseRepository.CreateOrUpdateAsync(entity, cancellationToken);

        return Success();
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productBaseRepository.DeleteByIdAsync(id, cancellationToken);

        return Success();
    }

    private async Task<ProductBaseEntity> MapToEntity(ProductBaseEventDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Map();
        var categoryId = await _categoryRepository.GetIdByExternalIdAsync(dto.CategoryId, cancellationToken);

        entity.CategoryId = categoryId.Value;

        return entity;
    }
}