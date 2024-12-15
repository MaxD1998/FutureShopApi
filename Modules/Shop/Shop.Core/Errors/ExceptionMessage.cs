using Shared.Infrastructure.Dtos;

namespace Shop.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorMessageDto PurchaseList001UserHasFavouireList => new("PurchaseList001", "User has favourite list");
}