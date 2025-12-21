using Shop.Domain.Entities.Products;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.ProductReview;

public class ProductReviewResponseFormDto : ProductReviewRequestFormDto
{
    public Guid Id { get; set; }

    public string Username { get; set; }

    public static Expression<Func<ProductReviewEntity, ProductReviewResponseFormDto>> Map() => entity => new()
    {
        Comment = entity.Comment,
        Id = entity.Id,
        ProductId = entity.ProductId,
        Rating = entity.Rating,
        Username = entity.User != null ? $"{entity.User.FirstName} {entity.User.LastName}" : null
    };
}