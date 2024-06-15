using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.ProductBase.Queries;

public record GetPageProductBaseDtoQuery(int PageNumber) : IRequest<PageDto<ProductBaseDto>>;

internal class GetPageProductBaseDtoQueryHandler : BaseRequestHandler<ProductContext, GetPageProductBaseDtoQuery, PageDto<ProductBaseDto>>
{
    public GetPageProductBaseDtoQueryHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<PageDto<ProductBaseDto>> Handle(GetPageProductBaseDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>()
            .Include(x => x.Category)
            .Include(x => x.Products)
            .Include(x => x.ProductParameters)
            .Select(x => new ProductBaseDto(x))
            .ToPageAsync(request.PageNumber, cancellationToken);
}