using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetPageCategoryListDtoQuery(int PageNumber) : IRequest<PageDto<CategoryListDto>>;

internal class GetPageCategoryListDtoQueryHandler : IRequestHandler<GetPageCategoryListDtoQuery, PageDto<CategoryListDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetPageCategoryListDtoQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<PageDto<CategoryListDto>> Handle(GetPageCategoryListDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .OrderBy(x => x.Translations.FirstOrDefault()!.Translation ?? x.Name)
            .Select(x => new CategoryListDto(x))
            .ToPageAsync(request.PageNumber, cancellationToken);
}