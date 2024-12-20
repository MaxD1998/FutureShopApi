using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Extensions;
using Shared.Core.Services;
using Shared.Infrastructure.Constants;
using Shop.Core.Dtos.Product;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Queries;
public record GetListProductShopListDtoByCategoryIdQuery(Guid CategoryId, ProductShopListFilterRequestDto Filter, Guid? FavouriteId) : IRequest<ResultDto<IEnumerable<ProductShopListDto>>>;

internal class GetListProductShopListDtoByCategoryIdQueryHandler : BaseService, IRequestHandler<GetListProductShopListDtoByCategoryIdQuery, ResultDto<IEnumerable<ProductShopListDto>>>
{
    private readonly ShopContext _context;
    private readonly IHeaderService _headerService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetListProductShopListDtoByCategoryIdQueryHandler(IHeaderService headerService, IHttpContextAccessor httpContextAccessor, ShopContext context)
    {
        _headerService = headerService;
        _httpContextAccessor = httpContextAccessor;
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<ProductShopListDto>>> Handle(GetListProductShopListDtoByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var categoryIds = await GetCategoryIds([request.CategoryId], cancellationToken);
        var userId = _httpContextAccessor.GetUserId();
        var results = await _context.Set<ProductEntity>()
            .AsNoTracking()
            .Where(x => categoryIds.Contains(x.ProductBase.CategoryId))
            .Filter(request.Filter, _headerService.GetHeader(HeaderNameConst.Lang))
            .Select(ProductShopListDto.Map(_headerService.GetHeader(HeaderNameConst.Lang), userId, request.FavouriteId))
            .ToListAsync(cancellationToken);

        return Success<IEnumerable<ProductShopListDto>>(results);
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
                results.AddRange(await GetCategoryIds(subCategories.Select(x => x.Id), cancellationToken));
        }

        return results;
    }
}