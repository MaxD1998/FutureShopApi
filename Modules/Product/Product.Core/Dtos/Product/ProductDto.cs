using Product.Domain.Entities;

namespace Product.Core.Dtos.Product;

public class ProductDto
{
    public ProductDto(ProductEntity entity)
    {
        FileIds = entity.ProductPhotos.Select(x => x.FileId);
        Id = entity.Id;
        IsInPurchaseList = entity.PurchaseListItems.Any();
        Name = entity.Translations?.FirstOrDefault()?.Translation ?? entity.Name;
        Price = entity.Price;
        ProductParameters = entity.ProductParameterValues.Select(x => new IdNameValueDto(x));
    }

    public IEnumerable<string> FileIds { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public IEnumerable<IdNameValueDto> ProductParameters { get; set; }
}