using FluentValidation;
using Shared.Dtos;

namespace Shared.Extensions;

public static class ValidationExtension
{
    public static IRuleBuilderOptions<T, TProperty> ErrorResponse<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ErrorMessageDto error)
        => rule.WithErrorCode(error.ErrorCode).WithMessage(error.ErrorMessage);
}