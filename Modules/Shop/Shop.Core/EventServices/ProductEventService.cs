using Shared.Core.Bases;
using Shop.Core.Dtos.Product;
using Shop.Infrastructure.Entities;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.EventServices;

public interface IProductEventService
{
    Task CreateOrUpdateAsync(ProductEventDto dto, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

internal class ProductEventService(IProductBaseRepository productBaseRepository, IProductRepository productRepository) : BaseService, IProductEventService
{
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task CreateOrUpdateAsync(ProductEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _productRepository.CreateOrUpdateForEventAsync(entity, cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _productRepository.DeleteByExternalIdAsync(externalId, cancellationToken);

    private async Task<ProductEntity> MapToEntity(ProductEventDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.Map();

        var productBaseId = await _productBaseRepository.GetIdByExternalIdAsync(dto.ProductBaseId, cancellationToken);

        if (!productBaseId.HasValue)
            throw new InvalidOperationException();

        entity.ProductBaseId = productBaseId.Value;

        var productId = await _productRepository.GetIdByExternalIdAsync(dto.Id, cancellationToken);

        if (productId.HasValue)
        {
            foreach (var productPhoto in entity.ProductPhotos)
                productPhoto.ProductId = productId.Value;
        }

        return entity;
    }
}