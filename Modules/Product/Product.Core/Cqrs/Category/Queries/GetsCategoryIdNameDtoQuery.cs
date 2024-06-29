using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetsCategoryIdNameDtoQuery : IRequest<IEnumerable<IdNameDto>>;

internal class GetsCategoryIdNameDtoQueryHandler : BaseRequestHandler<ProductContext, GetsCategoryIdNameDtoQuery, IEnumerable<IdNameDto>>
{
    private IHeaderService _headerService;

    public GetsCategoryIdNameDtoQueryHandler(IHeaderService headerService, ProductContext context) : base(context)
    {
        _headerService = headerService;
    }

    public override async Task<IEnumerable<IdNameDto>> Handle(GetsCategoryIdNameDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Select(x => new IdNameDto(x))
            .ToListAsync(cancellationToken);
}