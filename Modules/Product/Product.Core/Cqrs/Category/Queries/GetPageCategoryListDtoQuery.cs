using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetPageCategoryListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<CategoryListDto>>>;

internal class GetPageCategoryListDtoQueryHandler : BaseService, IRequestHandler<GetPageCategoryListDtoQuery, ResultDto<PageDto<CategoryListDto>>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetPageCategoryListDtoQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ResultDto<PageDto<CategoryListDto>>> Handle(GetPageCategoryListDtoQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .OrderBy(x => x.Translations.Select(x => x.Translation).FirstOrDefault() ?? x.Name)
            .Select(CategoryListDto.Map(_headerService.GetHeader(HeaderNameConst.Lang)))
            .ToPageAsync(request.PageNumber, cancellationToken);

        return Success(result);
    }
}