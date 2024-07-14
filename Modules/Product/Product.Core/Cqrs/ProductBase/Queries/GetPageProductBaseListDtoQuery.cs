using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.ProductBase.Queries;

public record GetPageProductBaseListDtoQuery(int PageNumber) : IRequest<PageDto<ProductBaseListDto>>;

internal class GetPageProductBaseListDtoQueryHandler : BaseRequestHandler<ProductContext, GetPageProductBaseListDtoQuery, PageDto<ProductBaseListDto>>
{
    public GetPageProductBaseListDtoQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<PageDto<ProductBaseListDto>> Handle(GetPageProductBaseListDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>()
            .Include(x => x.Category)
            .Include(x => x.Products)
            .Include(x => x.ProductParameters)
            .Select(x => new ProductBaseListDto(x))
            .ToPageAsync(request.PageNumber, cancellationToken);
}