using Shared.Infrastructure.Dtos;

namespace Shop.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorDto AdCampaignItem002OneOfFilesWasEmpty => new("AdCampaignItem002", "One of files was empty");

    public static ErrorDto PurchaseList001UserHasFavouireList => new("PurchaseList001", "User has favourite list");
}