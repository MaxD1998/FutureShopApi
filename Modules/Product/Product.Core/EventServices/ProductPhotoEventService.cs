using Product.Infrastructure.Repositories;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;

namespace Product.Core.EventServices;

public interface IProductPhotoEventService
{
    Task GetMissingFileIdsAsync(List<string> ids, CancellationToken cancellationToken);
}

internal class ProductPhotoEventService(IProductPhotoRepository productPhotoRepository, IRabbitMqContext rabbitMqContext) : IProductPhotoEventService
{
    private readonly IProductPhotoRepository _productPhotoRepository = productPhotoRepository;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;

    public async Task GetMissingFileIdsAsync(List<string> ids, CancellationToken cancellationToken)
    {
        var missingIds = await _productPhotoRepository.GetMissingFileIdsAsync(ids, cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleFilesToDelete, EventMessageDto.Create(missingIds, MessageType.DeleteRange));
    }
}