using Shop.Domain.Aggregates.Categories.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Shop.Domain.Aggregates.Categories.Comparers;

public class CategoryTranslateEntityComparer : IEqualityComparer<CategoryTranslationEntity>
{
    public static HashSet<CategoryTranslationEntity> CreateSet() => new HashSet<CategoryTranslationEntity>(new CategoryTranslateEntityComparer());

    public static HashSet<CategoryTranslationEntity> CreateSet(IEnumerable<CategoryTranslationEntity> translations) => translations.ToHashSet(new CategoryTranslateEntityComparer());

    public bool Equals(CategoryTranslationEntity x, CategoryTranslationEntity y) => x.Lang == y.Lang;

    public int GetHashCode([DisallowNull] CategoryTranslationEntity obj) => HashCode.Combine(obj.Lang);
}