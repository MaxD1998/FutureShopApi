﻿using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Authorization.Core.Dtos.Login;

public class LoginFormValidator : AbstractValidator<LoginFormDto>
{
    public LoginFormValidator()
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