using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Product.Queries;
public record GetPageProductListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<ProductListDto>>>;

internal class GetPageProductListDtoQueryHandler : BaseService, IRequestHandler<GetPageProductListDtoQuery, ResultDto<PageDto<ProductListDto>>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetPageProductListDtoQueryHandler(ProductPostgreSqlContext context)
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