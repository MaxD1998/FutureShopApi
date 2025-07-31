using Product.Domain.Aggregates.ProductBases;
using Product.Domain.Aggregates.Products.Comparers;
using Product.Domain.Aggregates.Products.Entities;
using Shared.Domain.Bases;
using Shared.Domain.Extensions;
using Shared.Domain.Interfaces;

namespace Product.Domain.Aggregates.Products;

public class ProductAggregate : BaseEntity, IUpdate<ProductAggregate>
{
    private HashSet<ProductPhotoEntity> _productPhotos = ProductPhotoEntityComparer.CreateSet();

    public ProductAggregate(string name, Guid productBaseId, IEnumerable<ProductPhotoEntity> productPhotos)
    {
        SetName(name);
        SetProductBaseId(productBaseId);
        SetProductPhotos(productPhotos);
    }

    private ProductAggregate()
    {
    }

    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseAggregate ProductBase { get; private set; }

    public IReadOnlyCollection<ProductPhotoEntity> ProductPhotos => _productPhotos;

    #endregion Related Data

    #region Setters

    public void SetName(string name)
    {
        ValidateRequiredLongStringProperty(nameof(Name), name);

        Name = name;
    }

    public void SetProductBaseId(Guid productBaseId)
    {
        ValidateRequiredProperty(nameof(ProductBaseId), productBaseId);

        ProductBaseId = productBaseId;
    }

    public void SetProductPhotos(IEnumerable<ProductPhotoEntity> productPhotos)
    {
        if (productPhotos == null)
            throw new ArgumentNullException(nameof(productPhotos));

        _productPhotos = ProductPhotoEntityComparer.CreateSet(productPhotos);
    }

    #endregion Setters

    #region Methods

    public void Update(ProductAggregate entity)
    {
        Name = entity.Name;

        var productPhotos = ProductPhotoEntityComparer.CreateSet(entity.ProductPhotos);
        _productPhotos.UpdateEntities(productPhotos);
    }

    #endregion Methods
}