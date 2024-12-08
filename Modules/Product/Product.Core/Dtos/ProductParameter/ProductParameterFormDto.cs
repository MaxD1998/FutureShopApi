using FluentValidation;
using Product.Core.Dtos.ProductParameterTranslation;
using Product.Domain.Entities;

namespace Product.Core.Dtos.ProductParameter;

public class ProductParameterFormDto
{
    public ProductParameterFormDto()
    {
    }

    public ProductParameterFormDto(ProductParameterEntity entity)
    {
        Id = entity.Id;
        Name = entity.Name;
        Translations = entity.Translations.Select(x => new ProgramParameterTranslationFormDto(x)).ToList();
    }

    public Guid? Id { get; set; }

    public string Name { get; set; }

    public List<ProgramParameterTranslationFormDto> Translations { get; set; }

    public ProductParameterEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Name = Name,
        Translations = Translations.Select(x => x.ToEntity()).ToList()
    };
}

public class ProductParameterFormValidator : AbstractValidator<ProductParameterFormDto>
{
    public ProductParameterFormValidator()
    {
    }
}