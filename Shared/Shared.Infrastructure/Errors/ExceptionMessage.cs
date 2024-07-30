using Shared.Infrastructure.Dtos;

namespace Shared.Infrastructure.Errors;

public static class ExceptionMessage
{
    public static ErrorMessageDto D001DatabaseNotAvailable => new("D001", "Database is not available");
}