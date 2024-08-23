using Shared.Infrastructure.Dtos;

namespace Product.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorMessageDto ProductPhoto001OoneOfFilesWasEmpty => new("ProductPhoto001", "One of files was empty");
}