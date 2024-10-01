using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Core.Interfaces.Services;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Extensions;
using Shared.Infrastructure.Constants;

namespace Product.Core.Cqrs.Product.Queries;
public record GetListProductShopListDtoByCategoryIdQuery(Guid CategoryId, ProductShopListFilterRequestDto Filter) : IRequest<IEnumerable<ProductShopListDto>>;

internal class GetListProductShopListDtoByCategoryIdQueryHandler : IRequestHandler<GetListProductShopListDtoByCategoryIdQuery, IEnumerable<ProductShopListDto>>
{
    private readonly ProductPostgreSqlContext _context;
    private readonly IHeaderService _headerService;

    public GetListProductShopListDtoByCategoryIdQueryHandler(IHeaderService headerService, ProductPostgreSqlContext context)
    {
        _headerService = headerService;
        _context = context;
    }

    public async Task<IEnumerable<ProductShopListDto>> Handle(GetListProductShopListDtoByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var categoryIds = await GetCategoryIds([request.CategoryId], cancellationToken);

        return await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Include(x => x.ProductPhotos.OrderBy(y => y.Position))
            .Include(x => x.Translations.Where(x => x.Lang == _headerService.GetHeader(HeaderNameConst.Lang)))
            .Where(x => categoryIds.Contains(x.ProductBase.CategoryId))
            .Filter(request.Filter, _headerService.GetHeader(HeaderNameConst.Lang))
            .Select(x => new ProductShopListDto(x))
            .ToListAsync(cancellationToken);
    }

    private async Task<IEnumerable<Guid>> GetCategoryIds(IEnumerable<Guid> categoryIds, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var results = new List<Guid>();
        var categories = await _context.Set<CategoryEntity>()
            .AsNoTracking()
            .Include(x => x.SubCategories)
            .Where(x => categoryIds.Contains(x.Id))
            .ToListAsync();

        if (categories.Count > 0)
        {
            results.AddRange(categories.Select(x => x.Id));

            var subCategories = categories.SelectMany(x => x.SubCategories).ToList();

            if (subCategories.Count > 0)
            {
                results.AddRange(await GetCategoryIds(subCategories.Select(x => x.Id), cancellationToken));
            }
        }

        return results;
    }
}