using MediatR;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shop.Core.Dtos.ProductBase;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Queries;

public record GetPageProductBaseListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<ProductBaseListDto>>>;

internal class GetPageProductBaseListDtoQueryHandler : BaseService, IRequestHandler<GetPageProductBaseListDtoQuery, ResultDto<PageDto<ProductBaseListDto>>>
{
    private readonly ShopPostgreSqlContext _context;

    public GetPageProductBaseListDtoQueryHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<PageDto<ProductBaseListDto>>> Handle(GetPageProductBaseListDtoQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>()
            .Select(ProductBaseListDto.Map())
            .ToPageAsync(request.PageNumber, cancellationToken);

        return Success(result);
    }
}