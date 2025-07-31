using System.Diagnostics.CodeAnalysis;

namespace Product.Domain.Aggregates.Categories.Comparers;

public class CategoryAggregateComparer : IEqualityComparer<CategoryAggregate>
{
    public static HashSet<CategoryAggregate> CreateSet(IEnumerable<CategoryAggregate> categories) => categories.ToHashSet(new CategoryAggregateComparer());

    public bool Equals(CategoryAggregate x, CategoryAggregate y) => x.Id == y.Id;

    public int GetHashCode([DisallowNull] CategoryAggregate obj) => HashCode.Combine(obj.Id);
}