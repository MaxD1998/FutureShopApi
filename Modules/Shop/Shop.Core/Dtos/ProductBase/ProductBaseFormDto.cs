using FluentValidation;
using Shared.Core.Errors;
using Shared.Core.Extensions;
using Shop.Core.Dtos.ProductParameter;
using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.ProductBase;

public class ProductBaseFormDto
{
    public Guid CategoryId { get; set; }

    public string Name { get; set; }

    public List<ProductParameterFormDto> ProductParameters { get; set; }

    public static Expression<Func<ProductBaseEntity, ProductBaseFormDto>> Map() => entity => new()
    {
        CategoryId = entity.CategoryId,
        Name = entity.Name,
        ProductParameters = entity.ProductParameters.AsQueryable().Select(ProductParameterFormDto.Map()).ToList(),
    };

    public ProductBaseEntity ToEntity() => new()
    {
        CategoryId = CategoryId,
        Name = Name,
        ProductParameters = ProductParameters.Select(x => x.ToEntity()).ToList(),
    };
}

public class ProductBaseValidator : AbstractValidator<ProductBaseFormDto>
{
    public ProductBaseValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
                .ErrorResponse(ErrorMessage.ValueWasEmpty);
    }
}