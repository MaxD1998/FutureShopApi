using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Category.Queries;

public record GetListCategoryIdNameDtoAvailableToBeParentCategoryQuery(Guid? Id, IEnumerable<Guid> ChildIds) : IRequest<ResultDto<IEnumerable<IdNameDto>>>;

internal class GetsCategoryIdNameDtoAvailableToBeParentCategoryQueryHandler : BaseService, IRequestHandler<GetListCategoryIdNameDtoAvailableToBeParentCategoryQuery, ResultDto<IEnumerable<IdNameDto>>>
{
    private readonly ProductPostgreSqlContext _context;

    public GetsCategoryIdNameDtoAvailableToBeParentCategoryQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<IdNameDto>>> Handle(GetListCategoryIdNameDtoAvailableToBeParentCategoryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Set<CategoryEntity>()
            .AsNoTracking()
            .AsQueryable();

        if (request.Id.HasValue)
            query = query.Where(x => x.Id != request.Id.Value);

        if (request.ChildIds.Any())
        {
            var childIds = request.ChildIds.Concat(await ExceptionIdsAsync(request.ChildIds, cancellationToken));
            query = query.Where(x => !childIds.Contains(x.Id));
        }

        var results = await query
            .Select(IdNameDto.MapFromCategory())
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<IdNameDto>>(results);
    }

    private async Task<IEnumerable<Guid>> ExceptionIdsAsync(IEnumerable<Guid> ids, CancellationToken cancellationToken = default)
    {
        var results = await _context.Set<CategoryEntity>()
            .Where(x => ids.Contains(x.Id))
            .SelectMany(x => x.SubCategories.Select(x => x.Id))
            .ToListAsync(cancellationToken);

        if (results.Count == 0)
            return [];

        return results.Concat(await ExceptionIdsAsync(results, cancellationToken));
    }
}