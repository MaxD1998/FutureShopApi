using Shared.Shared.Interfaces;
using Shop.Core.Enums;
using Shop.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.Product;

public class ProductShopListFilterRequestDto : IFilter<ProductEntity>
{
    public string Name { get; set; }

    public decimal? PriceFrom { get; set; }

    public decimal? PriceTo { get; set; }

    public ProductSortType SortType { get; set; }

    public IQueryable<ProductEntity> FilterExecute(IQueryable<ProductEntity> query, string lang)
    {
        var utcNow = DateTime.UtcNow;

        if (Name != null && Name != string.Empty)
            query = query.Where(x => x.Translations.Any(y => y.Lang == lang && y.Translation.ToLower().Contains(Name.ToLower())) || !x.Translations.Any(y => y.Lang == lang) && x.Name.ToLower().Contains(Name.ToLower()));

        if (PriceFrom != null)
            query = query.Where(x => x.Prices.Any(y => (!y.Start.HasValue || y.Start <= utcNow) && (!y.End.HasValue || utcNow < y.End) && y.Price >= PriceFrom));

        if (PriceTo != null)
            query = query.Where(x => x.Prices.Any(y => (!y.Start.HasValue || y.Start <= utcNow) && (!y.End.HasValue || utcNow < y.End) && y.Price <= PriceTo));

        Expression<Func<ProductEntity, bool>> orderByCondition = x => x.Prices.Any(y => (!y.Start.HasValue || y.Start <= utcNow) && (!y.End.HasValue || utcNow < y.End));

        query = SortType switch
        {
            ProductSortType.NameDesc => query.OrderByDescending(x => x.Translations.Where(y => y.Lang == lang).Select(y => y.Translation).FirstOrDefault() ?? x.Name),
            ProductSortType.PriceAsc => query.OrderBy(orderByCondition),
            ProductSortType.PriceDesc => query.OrderByDescending(orderByCondition),
            _ => query.OrderBy(x => x.Translations.Where(y => y.Lang == lang).Select(y => y.Translation).FirstOrDefault() ?? x.Name)
        };

        return query;
    }
}