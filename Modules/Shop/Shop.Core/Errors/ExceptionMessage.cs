using Shared.Infrastructure.Dtos;

namespace Shop.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorDto AdCampaignItem002OneOfFilesWasEmpty => new("AdCampaignItem002", "One of files was empty");

    public static ErrorDto ProductReview001UserCreatedReviewForThisProduct => new("ProductReview001", "User created review for this product.");

    public static ErrorDto ProductReview002UserIsNotAuthorizedToUpdateThisReview => new("ProductReview002", "User is not authorized to update this review.");

    public static ErrorDto PurchaseList001UserHasFavouireList => new("PurchaseList001", "User has favourite list");
}