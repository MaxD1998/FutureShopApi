using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;

namespace Product.Core.Cqrs.Category.Queries;
public record GetPageCategoryListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<CategoryListDto>>>;

internal class GetPageCategoryListDtoQueryHandler : BaseService, IRequestHandler<GetPageCategoryListDtoQuery, ResultDto<PageDto<CategoryListDto>>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetPageCategoryListDtoQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<PageDto<CategoryListDto>>> Handle(GetPageCategoryListDtoQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .Select(CategoryListDto.Map())
            .ToPageAsync(request.PageNumber, cancellationToken);

        return Success(result);
    }
}