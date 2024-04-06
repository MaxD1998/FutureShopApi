using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetsCategoryDtoQuery : IRequest<IEnumerable<CategoryDto>>;

internal class GetsCategoryDtoQueryHandler : BaseRequestHandler<ProductContext, GetsCategoryDtoQuery, IEnumerable<CategoryDto>>
{
    private readonly IHeaderService _headerService;

    public GetsCategoryDtoQueryHandler(IHeaderService headerService, ProductContext context) : base(context)
    {
        _headerService = headerService;
    }

    public override async Task<IEnumerable<CategoryDto>> Handle(GetsCategoryDtoQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Select(x => new CategoryDto(x))
            .ToListAsync();

        return result.OrderBy(x => x.Name);
    }
}