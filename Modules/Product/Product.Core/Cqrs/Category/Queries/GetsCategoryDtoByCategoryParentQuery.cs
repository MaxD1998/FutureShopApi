using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetsCategoryDtoByCategoryParentQuery(Guid? CategoryParentId = null) : IRequest<IEnumerable<CategoryDto>>;

internal class GetsCategoryDtoByCategoryParentQueryHandler : BaseRequestHandler<ProductContext, GetsCategoryDtoByCategoryParentQuery, IEnumerable<CategoryDto>>
{
    private readonly IHeaderService _headerService;

    public GetsCategoryDtoByCategoryParentQueryHandler(IHeaderService headerService, ProductContext context) : base(context)
    {
        _headerService = headerService;
    }

    public override async Task<IEnumerable<CategoryDto>> Handle(GetsCategoryDtoByCategoryParentQuery request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Where(x => x.ParentCategoryId == request.CategoryParentId)
            .Select(x => new CategoryDto(x))
            .ToListAsync();
}