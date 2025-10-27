namespace Shared.Shared.Enums;

public enum ShopPermission
{
    CategoryAddUpdate = 101,
    CategoryDelete = 102,
    CategoryRead = 103,

    ProductAddUpdateBase = 201,
    ProductBaseDelete = 202,
    ProductBaseRead = 203,

    ProductAddUpdate = 301,
    ProductDelete = 302,
    ProductRead = 303,

    AdCamaignAddUpdate = 401,
    AdCamaignDelete = 402,
    AdCamaignRead = 403,

    PromotionAddUpdate = 501,
    PromotionDelete = 502,
    PromotionRead = 503,
}