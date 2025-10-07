namespace Shop.Core.Interfaces;

public interface IProductPrice
{
    /// <summary>
    /// Id of a product
    /// </summary>
    public Guid Id { get; set; }

    public decimal OriginalPrice { get; set; }

    public decimal Price { get; set; }
}