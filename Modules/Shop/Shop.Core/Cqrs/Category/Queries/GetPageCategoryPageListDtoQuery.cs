using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Core.Services;
using Shop.Core.Dtos.Category;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Queries;
public record GetPageCategoryPageListDtoQuery(int PageNumber) : IRequest<ResultDto<PageDto<CategoryPageListDto>>>;

internal class GetPageCategoryPageListDtoQueryHandler : BaseService, IRequestHandler<GetPageCategoryPageListDtoQuery, ResultDto<PageDto<CategoryPageListDto>>>
{
    private readonly ShopPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetPageCategoryPageListDtoQueryHandler(IHeaderService headerService, ShopPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ResultDto<PageDto<CategoryPageListDto>>> Handle(GetPageCategoryPageListDtoQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .OrderBy(x => x.Translations.Select(x => x.Translation).FirstOrDefault() ?? x.Name)
            .Select(CategoryPageListDto.Map())
            .ToPageAsync(request.PageNumber, cancellationToken);

        return Success(result);
    }
}