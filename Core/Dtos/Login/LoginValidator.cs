using FluentValidation;
using Shared.Errors;
using Shared.Extensions;

namespace Core.Dtos.Login;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty)
            .EmailAddress()
                .ErrorResponse(ErrorMessage.IsNotEmail);

        RuleFor(x => x.Password)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}