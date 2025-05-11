using Shared.Infrastructure.Dtos;

namespace Shared.Core.Errors;

public static class CommonExceptionMessage
{
    public static ErrorDto C001SessionHasExpired => new("C001", "Session has expired");

    public static ErrorDto C002ValidatorNotExist => new("C002", "Validator not exist");

    public static ErrorDto C003WrongRefreshTokenFormat => new("C003", "Wrong refresh token format");

    public static ErrorDto C004WrongEmailOrPassword => new("C004", "Wrong email or password");

    public static ErrorDto C005BadGuidFormat => new("C005", "Bad Guid format");

    public static ErrorDto C006RecordAlreadyExists => new("C006", "Record already exists");

    public static ErrorDto C007RecordWasNotFound => new("C007", "Record was not found");
}