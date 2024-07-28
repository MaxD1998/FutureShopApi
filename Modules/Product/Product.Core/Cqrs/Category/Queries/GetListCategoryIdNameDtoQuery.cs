using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetListCategoryIdNameDtoQuery : IRequest<IEnumerable<IdNameDto>>;

internal class GetsCategoryIdNameDtoQueryHandler : IRequestHandler<GetListCategoryIdNameDtoQuery, IEnumerable<IdNameDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private IHeaderService _headerService;

    public GetsCategoryIdNameDtoQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<IEnumerable<IdNameDto>> Handle(GetListCategoryIdNameDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Select(x => new IdNameDto(x))
            .ToListAsync(cancellationToken);
}