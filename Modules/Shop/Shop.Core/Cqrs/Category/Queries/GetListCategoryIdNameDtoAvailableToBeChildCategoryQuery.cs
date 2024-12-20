using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Services;
using Shop.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Category.Queries;
public record GetListCategoryIdNameDtoAvailableToBeChildCategoryQuery(Guid? Id, Guid? ParentId, IEnumerable<Guid> ChildIds) : IRequest<ResultDto<IEnumerable<IdNameDto>>>;

internal class GetsCategoryIdNameDtoAvailableToBeChildCategoryQueryHandler : BaseService, IRequestHandler<GetListCategoryIdNameDtoAvailableToBeChildCategoryQuery, ResultDto<IEnumerable<IdNameDto>>>
{
    private readonly ShopContext _context;
    private readonly IHeaderService _headerService;

    public GetsCategoryIdNameDtoAvailableToBeChildCategoryQueryHandler(IHeaderService headerService, ShopContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<IdNameDto>>> Handle(GetListCategoryIdNameDtoAvailableToBeChildCategoryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Set<CategoryEntity>()
            .AsNoTracking()
            .AsQueryable();

        query = request.Id.HasValue
            ? query = query.Where(x => x.Id != request.Id && (x.ParentCategoryId == request.Id.Value || x.ParentCategoryId == null))
            : query = query.Where(x => x.ParentCategoryId == null);

        if (request.ParentId.HasValue)
        {
            var parentIds = await GetExceptionIdAsync(request.ParentId.Value, cancellationToken);
            query = query.Where(x => !parentIds.Contains(x.Id));
        }

        if (request.ChildIds.Any())
            query = query.Where(x => !request.ChildIds.Contains(x.Id));

        var results = await query
            .Select(IdNameDto.MapFromCategory())
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<IdNameDto>>(results);
    }

    private async Task<IEnumerable<Guid>> GetExceptionIdAsync(Guid parentId, CancellationToken cancellationToken = default)
    {
        var results = new List<Guid>();
        var exceptionCategory = await _context.Set<CategoryEntity>()
            .FirstOrDefaultAsync(x => x.Id == parentId, cancellationToken);

        if (exceptionCategory == null)
            return results;

        results.Add(exceptionCategory.Id);

        if (exceptionCategory.ParentCategoryId.HasValue)
            results.AddRange(await GetExceptionIdAsync((Guid)exceptionCategory.ParentCategoryId, cancellationToken));

        return results;
    }
}