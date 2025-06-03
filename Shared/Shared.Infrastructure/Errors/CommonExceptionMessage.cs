using Shared.Infrastructure.Dtos;

namespace Shared.Infrastructure.Errors;

public static class CommonExceptionMessage
{
    public static ErrorDto D001DatabaseNotAvailable => new("D001", "Database is not available");
}