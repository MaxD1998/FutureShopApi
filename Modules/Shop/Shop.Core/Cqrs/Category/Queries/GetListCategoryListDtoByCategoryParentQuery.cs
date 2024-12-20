using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Queries;
public record GetListCategoryListDtoByCategoryParentQuery(Guid? CategoryParentId = null) : IRequest<ResultDto<IEnumerable<CategoryListDto>>>;

internal class GetsCategoryDtoByCategoryParentQueryHandler : BaseService, IRequestHandler<GetListCategoryListDtoByCategoryParentQuery, ResultDto<IEnumerable<CategoryListDto>>>
{
    private readonly ShopContext _context;
    private readonly IHeaderService _headerService;

    public GetsCategoryDtoByCategoryParentQueryHandler(IHeaderService headerService, ShopContext context)
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