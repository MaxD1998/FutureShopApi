using Product.Domain.Aggregates.Products.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Product.Domain.Aggregates.Products.Comparers;

public class ProductPhotoEntityComparer : IEqualityComparer<ProductPhotoEntity>
{
    public static HashSet<ProductPhotoEntity> CreateSet() => new HashSet<ProductPhotoEntity>(new ProductPhotoEntityComparer());

    public static HashSet<ProductPhotoEntity> CreateSet(IEnumerable<ProductPhotoEntity> productPhotos) => productPhotos.ToHashSet(new ProductPhotoEntityComparer());

    public bool Equals(ProductPhotoEntity x, ProductPhotoEntity y) => x.FileId == y.FileId;

    public int GetHashCode([DisallowNull] ProductPhotoEntity obj) => HashCode.Combine(obj.FileId);
}