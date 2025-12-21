using Authorization.Domain.Entities.Users;
using Shared.Domain.Enums;
using Shared.Shared.Enums;

namespace Authorization.Core.Dtos;

public class AuthorizeDto
{
    public AuthorizeDto(UserEntity entity, string token)
    {
        Id = entity.Id;
        Username = $"{entity.FirstName} {entity.LastName}";
        Token = token;
        Roles = entity.Type.GetUserPrivileges();
        AuthorizationPermissions = entity.UserPermissionGroups.SelectMany(x => x.PermissionGroup.AuthorizationPermissions.Select(x => x.Permission)).ToList();
        ProductPermissions = entity.UserPermissionGroups.SelectMany(x => x.PermissionGroup.ProductPermissions.Select(x => x.Permission)).ToList();
        ShopPermissions = entity.UserPermissionGroups.SelectMany(x => x.PermissionGroup.ShopPermissions.Select(x => x.Permission)).ToList();
        WarehousePermissions = entity.UserPermissionGroups.SelectMany(x => x.PermissionGroup.WarehousePermissions.Select(x => x.Permission)).ToList();
    }

    public List<AuthorizationPermission> AuthorizationPermissions { get; set; }

    public Guid Id { get; }

    public List<ProductPermission> ProductPermissions { get; set; }

    public List<UserType> Roles { get; set; }

    public List<ShopPermission> ShopPermissions { get; set; }

    public string Token { get; }

    public string Username { get; }

    public List<WarehousePermission> WarehousePermissions { get; set; }
}