using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetListCategoryIdNameDtoQuery : IRequest<ResultDto<IEnumerable<IdNameDto>>>;

internal class GetsCategoryIdNameDtoQueryHandler : BaseService, IRequestHandler<GetListCategoryIdNameDtoQuery, ResultDto<IEnumerable<IdNameDto>>>
{
    private readonly ProductPostgreSqlContext _context;
    private IHeaderService _headerService;

    public GetsCategoryIdNameDtoQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<IdNameDto>>> Handle(GetListCategoryIdNameDtoQuery request, CancellationToken cancellationToken)
    {
        var results = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Select(IdNameDto.MapFromCategory(_headerService.GetHeader(HeaderNameConst.Lang)))
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<IdNameDto>>(results);
    }
}