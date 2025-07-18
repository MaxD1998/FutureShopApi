﻿using Product.Core.Dtos.Product;
using Product.Infrastructure.Repositories;
using Shared.Core.Bases;
using Shared.Core.Constans;
using Shared.Core.Dtos;
using Shared.Core.Enums;
using Shared.Infrastructure;
using Shared.Infrastructure.Extensions;

namespace Product.Core.Services;

public interface IProductService
{
    Task<ResultDto<ProductFormDto>> CreateAsync(ProductFormDto dto, CancellationToken cancellationToken);

    Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<ProductFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(int indexPage, CancellationToken cancellationToken);

    Task<ResultDto<ProductFormDto>> UpdateAsync(Guid id, ProductFormDto dto, CancellationToken cancellationToken);
}

public class ProductService(IProductRepository productRepository, IRabbitMqContext rabbitMqContext) : BaseService, IProductService
{
    private readonly IProductRepository _productRepository = productRepository;
    private readonly IRabbitMqContext _rabbitMqContext = rabbitMqContext;

    public async Task<ResultDto<ProductFormDto>> CreateAsync(ProductFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.CreateAsync(dto.ToEntity(), cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProduct, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _productRepository.GetByIdAsync(entity.Id, ProductFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteByIdAsync(id, cancellationToken);
        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProduct, EventMessageDto.Create(id, MessageType.Delete));

        return Success();
    }

    public async Task<ResultDto<ProductFormDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _productRepository.GetByIdAsync(id, ProductFormDto.Map(), cancellationToken);

        return Success(result);
    }

    public async Task<ResultDto<PageDto<ProductListDto>>> GetPageListAsync(int indexPage, CancellationToken cancellationToken)
    {
        var results = await _productRepository.GetPageAsync(indexPage, ProductListDto.Map(), cancellationToken);

        return Success(results);
    }

    public async Task<ResultDto<ProductFormDto>> UpdateAsync(Guid id, ProductFormDto dto, CancellationToken cancellationToken)
    {
        var entity = await _productRepository.UpdateAsync(id, dto.ToEntity(), cancellationToken);

        await _rabbitMqContext.SendMessageAsync(RabbitMqExchangeConst.ProductModuleProduct, EventMessageDto.Create(entity, MessageType.AddOrUpdate));

        var result = await _productRepository.GetByIdAsync(entity.Id, ProductFormDto.Map(), cancellationToken);

        return Success(result);
    }
}