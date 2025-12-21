using Authorization.Infrastructure.Entities.Permissions;
using Authorization.Infrastructure.Entities.Users;
using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;

namespace Authorization.Infrastructure.Entities.PrermissionGroups;

public class PermissionGroupEntity : BaseEntity, IUpdate<PermissionGroupEntity>, IEntityValidation
{
    public string Name { get; set; }

    #region Related Data

    public ICollection<AuthorizationPermissionEntity> AuthorizationPermissions { get; set; } = [];

    public ICollection<ProductPermissionEntity> ProductPermissions { get; set; } = [];

    public ICollection<ShopPermissionEntity> ShopPermissions { get; set; } = [];

    public ICollection<UserPermissionGroupEntity> UserPermissionGroups { get; set; } = [];

    public ICollection<WarehousePermissionEntity> WarehousePermissions { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(PermissionGroupEntity entity)
    {
        Name = entity.Name;
        AuthorizationPermissions.UpdateEntities(entity.AuthorizationPermissions);
        ProductPermissions.UpdateEntities(entity.ProductPermissions);
        ShopPermissions.UpdateEntities(entity.ShopPermissions);
        WarehousePermissions.UpdateEntities(entity.WarehousePermissions);
    }

    public void Validate()
    {
        ValidateName();
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new PropertyWasEmptyException(nameof(Name));

        var length = StringLengthConst.LongString;

        if (Name.Length > length)
            throw new PropertyWasTooLongException(nameof(Name), length);
    }

    #endregion Methods
}