using Shared.Infrastructure.Dtos;

namespace Shared.Core.Errors;

public static class CommonExceptionMessage
{
    public static ErrorMessageDto C001SessionHasExpired => new("C001", "Session has expired");

    public static ErrorMessageDto C002ValidatorNotExist => new("C002", "Validator not exist");

    public static ErrorMessageDto C003WrongRefreshTokenFormat => new("C003", "Wrong refresh token format");

    public static ErrorMessageDto C004WrongEmailOrPassword => new("C004", "Wrong email or password");

    public static ErrorMessageDto C005BadGuidFormat => new("C005", "Bad Guid format");

    public static ErrorMessageDto C006RecordAlreadyExists => new("C006", "Record already exists");

    public static ErrorMessageDto C007RecordWasNotFound => new("C007", "Record was not found");
}