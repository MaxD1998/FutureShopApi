using Shared.Infrastructure.Bases;
using System.Linq.Expressions;

namespace Shared.Core.Bases;

public abstract class BaseTranslationFormDto
{
    public Guid? Id { get; set; }

    public string Lang { get; set; }

    public string Translation { get; set; }
}

public abstract class BaseTranslationFormDto<TEntity, TFormDto> : BaseTranslationFormDto
    where TEntity : BaseTranslationEntity<TEntity>, new()
    where TFormDto : BaseTranslationFormDto, new()
{
    public static Expression<Func<TEntity, TFormDto>> Map() => entity => new TFormDto
    {
        Id = entity.Id,
        Lang = entity.Lang,
        Translation = entity.Translation
    };

    public TEntity ToEntity() => new()
    {
        Id = Id ?? Guid.Empty,
        Lang = Lang,
        Translation = Translation
    };
}