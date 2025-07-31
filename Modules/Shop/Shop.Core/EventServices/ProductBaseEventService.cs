using Shared.Core.Bases;
using Shop.Core.Dtos.ProductBase;
using Shop.Domain.Aggregates.ProductBases;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.EventServices;

public interface IProductBaseEventService
{
    Task CreateOrUpdateAsync(ProductBaseEventDto dto, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

public class ProductBaseEventService(ICategoryRepository categoryRepository, IProductBaseRepository productBaseRepository) : BaseService, IProductBaseEventService
{
    private readonly ICategoryRepository _categoryRepository = categoryRepository;
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;

    public async Task CreateOrUpdateAsync(ProductBaseEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _productBaseRepository.CreateOrUpdateAsync(entity, cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _productBaseRepository.DeleteByExternalIdAsync(externalId, cancellationToken);

    private async Task<ProductBaseAggregate> MapToEntity(ProductBaseEventDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Map();
        var categoryId = await _categoryRepository.GetIdByExternalIdAsync(dto.CategoryId, cancellationToken);

        entity.CategoryId = categoryId.Value;

        return entity;
    }
}