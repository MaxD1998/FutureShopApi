using Shared.Infrastructure.Dtos;

namespace Product.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorMessageDto Product001NoSynchronizedDataQueantityOfProductPhotosIsNotEqualWithRealFiles => new("Product001", "No synchronized data. Quantity of ProductPhotos is not equal with real files");
}