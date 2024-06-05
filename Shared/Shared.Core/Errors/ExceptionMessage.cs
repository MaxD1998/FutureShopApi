using Shared.Core.Dtos;

namespace Shared.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorMessageDto BadGuidFormat => new ErrorMessageDto("E007", "Bad Guid format");

    public static ErrorMessageDto DatabaseNotAvailable => new ErrorMessageDto("E005", "Database is not available");

    public static ErrorMessageDto NoDataToUpdate => new ErrorMessageDto("E001", "No data to update");

    public static ErrorMessageDto RecordAlreadyExists => new ErrorMessageDto("E008", "Record already exists");

    public static ErrorMessageDto SessionHasExpired => new ErrorMessageDto("E002", "Session has expired");

    public static ErrorMessageDto ValidatorNotExist => new ErrorMessageDto("E003", "Validator not exist");

    public static ErrorMessageDto WrongEmailOrPassword => new ErrorMessageDto("E006", "Wrong email or password");

    public static ErrorMessageDto WrongRefreshTokenFormat => new ErrorMessageDto("E004", "Wrong refresh token format");
}