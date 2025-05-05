using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure.Repositories;

namespace Shop.Core.EventServices;

public interface IProductEventService
{
    Task<ResultDto> CreateOrUpdateAsync(ProductEventDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}

public class ProductEventService(IProductBaseRepository productBaseRepository, IProductRepository productRepository) : BaseService, IProductEventService
{
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;
    private readonly IProductRepository _productRepository = productRepository;

    public async Task<ResultDto> CreateOrUpdateAsync(ProductEventDto dto, CancellationToken cancellationToken)
    {
        var entity = await MapToEntity(dto, cancellationToken);
        await _productRepository.CreateOrUpdateForEventAsync(entity, cancellationToken);

        return Success();
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteByIdAsync(id, cancellationToken);

        return Success();
    }

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