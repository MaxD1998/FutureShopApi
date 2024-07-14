﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Product.Queries;
public record GetPageProductListDtoQuery(int PageNumber) : IRequest<PageDto<ProductListDto>>;

internal class GetPageProductListDtoQueryHandler : BaseRequestHandler<ProductContext, GetPageProductListDtoQuery, PageDto<ProductListDto>>
{
    public GetPageProductListDtoQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<PageDto<ProductListDto>> Handle(GetPageProductListDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Include(x => x.ProductBase.ProductParameters)
            .Include(x => x.ProductParameterValues)
            .Include(x => x.Translations)
            .Select(x => new ProductListDto(x))
            .ToPageAsync(request.PageNumber, cancellationToken);
}