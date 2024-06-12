using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetPageCategoryDtoQuery(int PageNumber) : IRequest<PageDto<CategoryDto>>;

internal class GetPageCategoryDtoQueryHandler : BaseRequestHandler<ProductContext, GetPageCategoryDtoQuery, PageDto<CategoryDto>>
{
    private readonly IHeaderService _headerService;

    public GetPageCategoryDtoQueryHandler(IHeaderService headerService, ProductContext context) : base(context)
    {
        _headerService = headerService;
    }

    public override async Task<PageDto<CategoryDto>> Handle(GetPageCategoryDtoQuery request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .OrderBy(x => x.Translations.FirstOrDefault()!.Translation ?? x.Name)
            .Select(x => new CategoryDto(x))
            .ToPageAsync(request.PageNumber, cancellationToken);
}