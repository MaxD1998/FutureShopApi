using Shared.Infrastructure.Dtos;

namespace Shared.Infrastructure.Errors;

public static class CommonExceptionMessage
{
    public static ErrorMessageDto D001DatabaseNotAvailable => new("D001", "Database is not available");
}