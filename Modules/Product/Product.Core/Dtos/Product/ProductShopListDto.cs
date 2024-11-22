using Product.Domain.Entities;

namespace Product.Core.Dtos.Product;

public class ProductShopListDto
{
    public ProductShopListDto(ProductEntity entity)
    {
        FileId = entity.ProductPhotos.FirstOrDefault()?.FileId;
        Id = entity.Id;
        IsInPurchaseList = entity.PurchaseListItems.Any();
        Name = entity.Translations?.FirstOrDefault()?.Translation ?? entity.Name;
        Price = entity.Price;
    }

    public string FileId { get; set; }

    public Guid Id { get; set; }

    public bool IsInPurchaseList { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }
}