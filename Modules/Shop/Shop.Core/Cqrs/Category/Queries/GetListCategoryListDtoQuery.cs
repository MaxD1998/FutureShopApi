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
public record GetListCategoryListDtoQuery : IRequest<ResultDto<IEnumerable<CategoryListDto>>>;

internal class GetListCategoryListDtoQueryHandler : BaseService, IRequestHandler<GetListCategoryListDtoQuery, ResultDto<IEnumerable<CategoryListDto>>>
{
    private readonly ShopPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetListCategoryListDtoQueryHandler(IHeaderService headerService, ShopPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<CategoryListDto>>> Handle(GetListCategoryListDtoQuery request, CancellationToken cancellationToken)
    {
        var results = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Select(CategoryListDto.Map(_headerService.GetHeader(HeaderNameConst.Lang)))
            .ToListAsync(cancellationToken);

        return Success(CategoryListDto.BuildTree(results));
    }
}