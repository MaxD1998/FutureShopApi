using Product.Core.Dtos.ProductParameterValue;
using Product.Core.Dtos.ProductTranslation;
using Product.Domain.Entities;

namespace Product.Core.Dtos.Product;

public class ProductFormDto
{
    public ProductFormDto()
    {
    }

    public ProductFormDto(ProductEntity entity)
    {
        Description = entity.Description;
        Name = entity.Name;
        Price = entity.Price;
        ProductBaseId = entity.ProductBaseId;
        ProductParameterValues = entity.ProductParameterValues.Select(x => new ProductParameterValueFormDto(x)).ToList();
        ProductPhotos = entity.ProductPhotos.Select(x => x.FileId).ToList();
        Translations = entity.Translations.Select(x => new ProductTranslationFormDto(x)).ToList();
    }

    public string Description { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public Guid ProductBaseId { get; set; }

    public List<ProductParameterValueFormDto> ProductParameterValues { get; set; }

    public List<string> ProductPhotos { get; set; }

    public List<ProductTranslationFormDto> Translations { get; set; }

    public ProductEntity ToEntity() => new()
    {
        Description = Description,
        Name = Name,
        Price = Price,
        ProductBaseId = ProductBaseId,
        ProductParameterValues = ProductParameterValues.Select(x => x.ToEntity()).ToList(),
        ProductPhotos = ProductPhotos.Select(ToProductPhotoEntity).ToList(),
        Translations = Translations.Select(x => x.ToEntity()).ToList(),
    };

    private static ProductPhotoEntity ToProductPhotoEntity(string fileId, int position) => new()
    {
        FileId = fileId,
        Position = position,
    };
}