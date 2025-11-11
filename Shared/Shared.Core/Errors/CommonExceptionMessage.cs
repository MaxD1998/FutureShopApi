using Shared.Infrastructure.Dtos;

namespace Shared.Core.Errors;

public static class CommonExceptionMessage
{
    public static ErrorDto C001SessionHasExpired => new("C001", "Session has expired.");

    public static ErrorDto C002BadGuidFormat => new("C002", "Bad Guid format.");

    public static ErrorDto C003RecordAlreadyExists => new("C003", "Record already exists.");

    public static ErrorDto C004RecordWasNotFound => new("C004", "Record was not found.");

    public static ErrorDto C005UserIsNotTheOwnerOfThisRecord => new("C005", "User is not the owner of this record.");
}