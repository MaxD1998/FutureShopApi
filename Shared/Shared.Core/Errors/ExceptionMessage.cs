using Shared.Core.Dtos;

namespace Shared.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorMessageDto BadGuidFormat => new("E007", "Bad Guid format");

    public static ErrorMessageDto DatabaseNotAvailable => new("E005", "Database is not available");

    public static ErrorMessageDto NoDataToUpdate => new("E001", "No data to update");

    public static ErrorMessageDto RecordAlreadyExists => new("E008", "Record already exists");

    public static ErrorMessageDto SessionHasExpired => new("E002", "Session has expired");

    public static ErrorMessageDto ValidatorNotExist => new("E003", "Validator not exist");

    public static ErrorMessageDto WrongEmailOrPassword => new("E006", "Wrong email or password");

    public static ErrorMessageDto WrongRefreshTokenFormat => new("E004", "Wrong refresh token format");
}