using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetListCategoryListDtoByCategoryParentQuery(Guid? CategoryParentId = null) : IRequest<ResultDto<IEnumerable<CategoryListDto>>>;

internal class GetsCategoryDtoByCategoryParentQueryHandler : BaseService, IRequestHandler<GetListCategoryListDtoByCategoryParentQuery, ResultDto<IEnumerable<CategoryListDto>>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetsCategoryDtoByCategoryParentQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<CategoryListDto>>> Handle(GetListCategoryListDtoByCategoryParentQuery request, CancellationToken cancellationToken)
    {
        var results = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Where(x => x.ParentCategoryId == request.CategoryParentId)
            .Select(CategoryListDto.Map(_headerService.GetHeader(HeaderNameConst.Lang)))
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<CategoryListDto>>(results);
    }
}