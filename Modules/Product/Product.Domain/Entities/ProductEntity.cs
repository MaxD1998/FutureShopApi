using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductEntity : BaseTranslatableEntity<ProductTranslationEntity>
{
    public string Description { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public ICollection<ProductPhotoEntity> ProductPhotos { get; set; } = [];

    #endregion Related Data

    public void Update(ProductEntity entity)
    {
        Description = entity.Description;
        Name = entity.Name;
        Price = entity.Price;
        ProductPhotos = entity.ProductPhotos;
        UpdateProductParameterValues(entity.ProductParameterValues);
        UpdateTranslations(entity.Translations);
    }

    private void UpdateProductParameterValues(IEnumerable<ProductParameterValueEntity> entities)
    {
        foreach (var productParameterValue in entities)
        {
            var result = ProductParameterValues.FirstOrDefault(x => x.ProductParameterId == productParameterValue.ProductParameterId);
            if (result is null)
            {
                ProductParameterValues.Add(productParameterValue);
                continue;
            }

            result.Update(productParameterValue);
        }

        foreach (var productParameterValue in ProductParameterValues.ToList())
        {
            if (!entities.Any(x => x.ProductParameterId == productParameterValue.ProductParameterId))
                ProductParameterValues.Remove(productParameterValue);
        }
    }
}