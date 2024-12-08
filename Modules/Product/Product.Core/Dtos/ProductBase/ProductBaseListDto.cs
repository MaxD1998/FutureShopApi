using FluentValidation;
using Product.Domain.Entities;
using Shared.Core.Errors;
using Shared.Core.Extensions;

namespace Product.Core.Dtos.ProductBase;

public class ProductBaseListDto
{
    public ProductBaseListDto(ProductBaseEntity entity)
    {
        CategoryName = entity.Category.Name;
        Id = entity.Id;
        Name = entity.Name;
        ProductCount = entity.Products.Count();
        ProductParameterCount = entity.ProductParameters.Count();
    }

    public string CategoryName { get; }

    public Guid Id { get; set; }

    public string Name { get; }

    public int ProductCount { get; }

    public int ProductParameterCount { get; }
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