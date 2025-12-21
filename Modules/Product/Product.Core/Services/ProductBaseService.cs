using Product.Core.Dtos;
using Product.Core.Dtos.ProductBase;
using Product.Infrastructure.Repositories;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;
using Shared.Shared.Dtos;

namespace Product.Core.Services;

public interface IProductBaseService
{
    Task<ResultDto<ProductBaseResponseFormDto>> CreateAsync(ProductBaseRequestFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken);

    Task<ResultDto<ProductBaseResponseFormDto>> UpdateAsync(Guid id, ProductBaseRequestFormDto dto, CancellationToken cancellationToken);
}

internal class ProductBaseService(IProductBaseRepository productBaseRepository, IRabbitMqContext rabbitMqContext) : IProductBaseService
{
    private readonly IProductBaseRepository _productBaseRepository = productBaseRepository;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;

    public async Task<ResultDto<ProductBaseResponseFormDto>> CreateAsync(ProductBaseRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productBaseRepository.CreateAsync(dto.ToEntity(), cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _productBaseRepository.GetByIdAsync(entity.Id, ProductBaseResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productBaseRepository.DeleteByIdAsync(id, cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(id, MessageType.Delete));

        return ResultDto.Success();
    }

    public async Task<ResultDto<ProductBaseResponseFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productBaseRepository.GetByIdAsync(id, ProductBaseResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }

    public async Task<ResultDto<IdNameDto>> GetIdNameByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetByIdAsync(id, IdNameDto.MapFromProductBase(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<List<IdNameDto>>> GetListIdNameAsync(CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetListAsync(IdNameDto.MapFromProductBase(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<PageDto<ProductBaseListDto>>> GetPageListAsync(PaginationDto pagination, CancellationToken cancellationToken)
    {
        var results = await _productBaseRepository.GetPageAsync(pagination, ProductBaseListDto.Map(), cancellationToken);

        return ResultDto.Success(results);
    }

    public async Task<ResultDto<ProductBaseResponseFormDto>> UpdateAsync(Guid id, ProductBaseRequestFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productBaseRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProductBase, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _productBaseRepository.GetByIdAsync(entity.Id, ProductBaseResponseFormDto.Map(), cancellationToken);

        return ResultDto.Success(result);
    }
}