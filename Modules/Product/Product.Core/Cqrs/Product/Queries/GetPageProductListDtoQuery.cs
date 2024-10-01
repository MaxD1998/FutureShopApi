using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Product.Queries;
public record GetPageProductListDtoQuery(int PageNumber) : IRequest<PageDto<ProductListDto>>;

internal class GetPageProductListDtoQueryHandler : IRequestHandler<GetPageProductListDtoQuery, PageDto<ProductListDto>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetPageProductListDtoQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<PageDto<ProductListDto>> Handle(GetPageProductListDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Include(x => x.ProductBase.ProductParameters)
            .Include(x => x.ProductParameterValues)
            .Include(x => x.Translations)
            .Select(x => new ProductListDto(x))
            .ToPageAsync(request.PageNumber, cancellationToken);
}