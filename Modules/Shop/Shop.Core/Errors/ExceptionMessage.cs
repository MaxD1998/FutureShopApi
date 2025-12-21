using Shared.Core.Dtos;

namespace Shop.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorDto ProductReview001UserCreatedReviewForThisProduct => new("ProductReview001", "User created review for this product.");

    public static ErrorDto ProductReview002UserIsNotAuthorizedToUpdateThisReview => new("ProductReview002", "User is not authorized to update this review.");
}