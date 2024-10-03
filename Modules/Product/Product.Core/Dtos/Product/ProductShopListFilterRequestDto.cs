using Product.Core.Enums;
using Product.Domain.Entities;
using Shared.Core.Interfaces;

namespace Product.Core.Dtos.Product;

public class ProductShopListFilterRequestDto : IFilter<ProductEntity>
{
    public string Name { get; set; }

    public decimal? PriceFrom { get; set; }

    public decimal? PriceTo { get; set; }

    public ProductSortType SortType { get; set; }

    public IQueryable<ProductEntity> FilterExecute(IQueryable<ProductEntity> query, string lang)
    {
        if (Name != null && Name != string.Empty)
            query = query.Where(x => x.Translations.Any(y => y.Lang == lang && y.Translation.ToLower().Contains(Name.ToLower())) || (!x.Translations.Any(y => y.Lang == lang) && x.Name.ToLower().Contains(Name.ToLower())));

        if (PriceFrom != null)
            query = query.Where(x => x.Price >= PriceFrom);

        if (PriceTo != null)
            query = query.Where(x => x.Price <= PriceTo);

        query = SortType switch
        {
            ProductSortType.NameDesc => query.OrderByDescending(x => x.Translations.Where(y => y.Lang == lang).Select(y => y.Translation).FirstOrDefault() ?? x.Name),
            ProductSortType.PriceAsc => query.OrderBy(x => x.Price),
            ProductSortType.PriceDesc => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Translations.Where(y => y.Lang == lang).Select(y => y.Translation).FirstOrDefault() ?? x.Name)
        };

        return query;
    }
}