using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shop.Infrastructure.Persistence.Entities.Users;

namespace Shop.Infrastructure.Persistence.Entities.Products;

public class ProductReviewEntity : BaseEntity, IUpdate<ProductReviewEntity>, IEntityValidation
{
    public string Comment { get; set; }

    public Guid ProductId { get; set; }

    public int Rating { get; set; }

    public Guid UserId { get; set; }

    #region Related Data

    public ProductEntity Product { get; set; }

    public UserEntity User { get; set; }

    #endregion Related Data

    public void Update(ProductReviewEntity entity)
    {
        Comment = entity.Comment;
        Rating = entity.Rating;
    }

    public void Validate()
    {
        ValidateProductId();
        ValidateRating();
        ValidateUserId();
    }

    private void ValidateProductId()
    {
        if (ProductId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(ProductId));
    }

    private void ValidateRating()
    {
        const int min = 1;
        const int max = 5;

        if (Rating < min || Rating > max)
            throw new PropertyWasOutOfRangeException(nameof(Rating), min, max);
    }

    private void ValidateUserId()
    {
        if (UserId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(UserId));
    }
}