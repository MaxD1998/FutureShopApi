using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;

public record GetsCategoryIdNameDtoAvailableToBeParentCategoryQuery(Guid? Id, IEnumerable<Guid> ChildIds) : IRequest<IEnumerable<IdNameDto>>;

internal class GetsCategoryIdNameDtoAvailableToBeParentCategoryQueryHandler : BaseRequestHandler<ProductContext, GetsCategoryIdNameDtoAvailableToBeParentCategoryQuery, IEnumerable<IdNameDto>>
{
    private readonly IHeaderService _headerService;

    public GetsCategoryIdNameDtoAvailableToBeParentCategoryQueryHandler(IHeaderService headerService, ProductContext context) : base(context)
    {
        _headerService = headerService;
    }

    public override async Task<IEnumerable<IdNameDto>> Handle(GetsCategoryIdNameDtoAvailableToBeParentCategoryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(x => x.Id != request.Id.Value);

        if (request.ChildIds.Any())
        {
            var childIds = request.ChildIds.Concat(await ExceptionIdsAsync(request.ChildIds, cancellationToken));
            query = query.Where(x => !childIds.Contains(x.Id));
        }

        var results = await query
            .Select(x => new IdNameDto(x))
            .ToListAsync(cancellationToken);

        return results;
    }

    private async Task<IEnumerable<Guid>> ExceptionIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var results = await _context.Set<CategoryEntity>()
            .Include(x => x.SubCategories)
            .Where(x => ids.Contains(x.Id))
            .SelectMany(x => x.SubCategories.Select(x => x.Id))
            .ToListAsync(cancellationToken);

        if (results.Count == 0)
            return [];

        return results.Concat(await ExceptionIdsAsync(results, cancellationToken));
    }
}