using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Queries;
public record GetPageCategoryListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<CategoryListDto>>>;

internal class GetPageCategoryListDtoQueryHandler : BaseService, IRequestHandler<GetPageCategoryListDtoQuery, ResultDto<PageDto<CategoryListDto>>>
{
    private readonly ShopContext _context;
    private readonly IHeaderService _headerService;

    public GetPageCategoryListDtoQueryHandler(IHeaderService headerService, ShopContext context)
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