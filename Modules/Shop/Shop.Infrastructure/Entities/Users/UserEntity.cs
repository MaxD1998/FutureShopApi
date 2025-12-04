using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Products;

namespace Shop.Infrastructure.Entities.Users;

public class UserEntity : BaseExternalEntity, IUpdate<UserEntity>, IUpdateEvent<UserEntity>
{
    public DateOnly DateOfBirth { get; set; }

    public string Email { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    #region Related Data

    public ICollection<ProductReviewEntity> ProductReviews { get; set; } = [];

    public ICollection<UserCompanyDetailsEntity> UserCompanyDetails { get; set; }

    public ICollection<UserDeliveryAddressEntity> UserDeliveryAddresses { get; set; }

    #endregion Related Data

    public void Update(UserEntity entity)
    {
    }

    public void UpdateEvent(UserEntity entity)
    {
        FirstName = entity.FirstName;
        LastName = entity.LastName;
    }
}