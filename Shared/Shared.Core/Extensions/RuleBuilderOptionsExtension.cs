using FluentValidation;
using Shared.Core.Dtos;

namespace Shared.Core.Extensions;

public static class RuleBuilderOptionsExtension
{
    public static IRuleBuilderOptions<T, TProperty> ErrorResponse<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, ErrorMessageDto error)
        => rule.WithErrorCode(error.ErrorCode).WithMessage(error.ErrorMessage);
}