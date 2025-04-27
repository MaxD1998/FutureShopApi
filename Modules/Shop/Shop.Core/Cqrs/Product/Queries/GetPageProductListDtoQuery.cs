using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Queries;
public record GetPageProductListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<ProductListDto>>>;

internal class GetPageProductListDtoQueryHandler : BaseService, IRequestHandler<GetPageProductListDtoQuery, ResultDto<PageDto<ProductListDto>>>
{
    private readonly ShopPostgreSqlContext _context;

    public GetPageProductListDtoQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<PageDto<ProductListDto>>> Handle(GetPageProductListDtoQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Select(ProductListDto.Map())
            .ToPageAsync(request.PageNumber, cancellationToken);

        return Success(result);
    }
}