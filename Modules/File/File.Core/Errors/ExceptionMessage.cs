using Shared.Infrastructure.Dtos;

namespace File.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorDto ProductPhoto001OneOfFilesWasEmpty => new("ProductPhoto001", "One of files was empty");
}