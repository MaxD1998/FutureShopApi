using MediatR;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.ProductBase.Queries;

public record GetPageProductBaseListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<ProductBaseListDto>>>;

internal class GetPageProductBaseListDtoQueryHandler : BaseService, IRequestHandler<GetPageProductBaseListDtoQuery, ResultDto<PageDto<ProductBaseListDto>>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetPageProductBaseListDtoQueryHandler(ProductPostgreSqlContext context)
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