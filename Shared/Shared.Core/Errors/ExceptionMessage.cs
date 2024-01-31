using Shared.Core.Dtos;

namespace Shared.Core.Errors;

public static class ExceptionMessage
{
    public static ErrorMessageDto NoDataToUpdate => new ErrorMessageDto("E001", "No data to update");

    public static ErrorMessageDto SessionHasExpired => new ErrorMessageDto("E002", "Session has expired");

    public static ErrorMessageDto UnknownContextType => new ErrorMessageDto("E003", "Unknown context type");

    public static ErrorMessageDto ValidatorNotExist => new ErrorMessageDto("E004", "Validator not exist");

    public static ErrorMessageDto WrongRefreshTokenFormat => new ErrorMessageDto("E005", "Wrong refresh token format");

    public static ErrorMessageDto DatabaseNotAvailable => new ErrorMessageDto("E006", "Database is not available");

    public static ErrorMessageDto WrongEmailOrPassword => new ErrorMessageDto("E007", "Wrong email or password");
}