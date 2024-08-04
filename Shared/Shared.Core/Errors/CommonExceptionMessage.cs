using Shared.Infrastructure.Dtos;

namespace Shared.Core.Errors;

public static class CommonExceptionMessage
{
    public static ErrorMessageDto E001SessionHasExpired => new("E001", "Session has expired");

    public static ErrorMessageDto E002ValidatorNotExist => new("E002", "Validator not exist");

    public static ErrorMessageDto E003WrongRefreshTokenFormat => new("E003", "Wrong refresh token format");

    public static ErrorMessageDto E004WrongEmailOrPassword => new("E004", "Wrong email or password");

    public static ErrorMessageDto E005BadGuidFormat => new("E005", "Bad Guid format");

    public static ErrorMessageDto E006RecordAlreadyExists => new("E006", "Record already exists");
}