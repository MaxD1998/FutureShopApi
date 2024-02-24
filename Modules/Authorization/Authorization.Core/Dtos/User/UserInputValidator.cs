using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using Shared.Infrastructure.Constants;

namespace Authorization.Core.Dtos.User;

public class UserInputValidator : AbstractValidator<UserInputDto>
{
    public UserInputValidator()
    {
        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);

        RuleFor(x => x.Email)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty)
            .EmailAddress()
                .ErrorResponse(ErrorMessage.IsNotEmail)
            .MaximumLength(StringLengthConst.LongString)
                .ErrorResponse(ErrorMessage.TooLongString);

        RuleFor(x => x.FirstName)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty)
            .MaximumLength(StringLengthConst.MiddleString)
                .ErrorResponse(ErrorMessage.TooLongString);

        RuleFor(x => x.LastName)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty)
            .MaximumLength(StringLengthConst.LongString)
                .ErrorResponse(ErrorMessage.TooLongString);

        RuleFor(x => x.Password)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(StringLengthConst.ShortString)
                .ErrorResponse(ErrorMessage.TooLongString);
    }
}