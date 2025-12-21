using Shop.Domain.Entities.Products;

namespace Shop.Core.Dtos.ProductReview;

public class ProductReviewRequestFormDto
{
    public string Comment { get; set; }

    public Guid ProductId { get; set; }

    public int Rating { get; set; }

    public ProductReviewEntity ToEntity(Guid userId) => new()
    {
        Comment = Comment,
        ProductId = ProductId,
        Rating = Rating,
        UserId = userId
    };
}