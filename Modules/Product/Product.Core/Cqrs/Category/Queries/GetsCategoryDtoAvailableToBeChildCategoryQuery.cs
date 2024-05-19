﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Category.Queries;
public record GetsCategoryDtoAvailableToBeChildCategoryQuery(Guid? Id, Guid? ParentId, IEnumerable<Guid> ChildIds) : IRequest<IEnumerable<CategoryDto>>;

internal class GetsCategoryDtoAvailableToBeChildCategoryQueryHandler : BaseRequestHandler<ProductContext, GetsCategoryDtoAvailableToBeChildCategoryQuery, IEnumerable<CategoryDto>>
{
    private readonly IHeaderService _headerService;

    public GetsCategoryDtoAvailableToBeChildCategoryQueryHandler(IHeaderService headerService, ProductContext context) : base(context)
    {
        _headerService = headerService;
    }

    public override async Task<IEnumerable<CategoryDto>> Handle(GetsCategoryDtoAvailableToBeChildCategoryQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .AsQueryable();

        query = request.Id.HasValue
            ? query = query.Where(x => x.Id != request.Id && (x.ParentCategoryId == request.Id.Value || x.ParentCategoryId == null))
            : query = query.Where(x => x.ParentCategoryId == null);

        if (request.ParentId.HasValue)
        {
            var parentIds = await GetExceptionIdAsync(request.ParentId.Value);
            query = query.Where(x => !parentIds.Contains(x.Id));
        }

        if (request.ChildIds.Any())
            query = query.Where(x => !request.ChildIds.Contains(x.Id));

        var results = await query
            .Select(x => new CategoryDto(x))
            .ToListAsync();

        return results;
    }

    private async Task<IEnumerable<Guid>> GetExceptionIdAsync(Guid parentId)
    {
        var results = new List<Guid>();
        var exceptionCategory = await _context.Set<CategoryEntity>()
            .FirstOrDefaultAsync(x => x.Id == parentId);

        if (exceptionCategory == null)
            return results;

        results.Add(exceptionCategory.Id);

        if (exceptionCategory.ParentCategoryId.HasValue)
            results.AddRange(await GetExceptionIdAsync((Guid)exceptionCategory.ParentCategoryId));

        return results;
    }
}