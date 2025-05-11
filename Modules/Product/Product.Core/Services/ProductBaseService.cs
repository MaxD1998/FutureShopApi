using Product.Core.Dtos;
using Product.Core.Dtos.ProductBase;
using Product.Infrastructure.Repositories;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;

namespace Product.Core.Services;

public interface IProductBaseService
{
    Task<ResultDto<ProductBaseFormDto>> CreateAsync(ProductBaseFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageListAsync(int pageIndex, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseFormDto>> UpdateAsync(Guid id, ProductBaseFormDto dto, CancellationToken cancellationToken);
}

public class ProductBaseService(IProductBaseRepository productBaseRepository, IRabbitMqContext rabbitMqContext) : BaseService, IProductBaseService
{
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;

    public async Task<ResultDto<ProductBaseFormDto>> CreateAsync(ProductBaseFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productBaseRepository.CreateAsync(dto.ToEntity(), cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _productBaseRepository.GetByIdAsync(entity.Id, ProductBaseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productBaseRepository.DeleteByIdAsync(id, cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(id, MessageType.Delete));

        return Success();
    }

    public async Task<ResultDto<ProductBaseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productBaseRepository.GetByIdAsync(id, ProductBaseFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetByIdAsync(id, IdNameDto.MapFromProductBase(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetListAsync(IdNameDto.MapFromProductBase(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageListAsync(int pageIndex, CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetPageAsync(pageIndex, ProductBaseListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<ProductBaseFormDto>> UpdateAsync(Guid id, ProductBaseFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productBaseRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _productBaseRepository.GetByIdAsync(entity.Id, ProductBaseFormDto.Map(), cancellationToken);

        return Success(result);
    }
}