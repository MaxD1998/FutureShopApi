using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetListCategoryListDtoByCategoryParentQuery(Guid? CategoryParentId = null) : IRequest<IEnumerable<CategoryListDto>>;

internal class GetsCategoryDtoByCategoryParentQueryHandler : IRequestHandler<GetListCategoryListDtoByCategoryParentQuery, IEnumerable<CategoryListDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetsCategoryDtoByCategoryParentQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<IEnumerable<CategoryListDto>> Handle(GetListCategoryListDtoByCategoryParentQuery request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Where(x => x.ParentCategoryId == request.CategoryParentId)
            .Select(x => new CategoryListDto(x))
            .ToListAsync(cancellationToken);
}