using Shared.Domain.Bases;

namespace Product.Domain.Entities;

public class ProductParameterEntity : BaseEntity
{
    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    #region Related Data

    public ProductBaseEntity ProductBase { get; set; }

    public ICollection<ProductParameterTranslationEntity> Translations { get; set; }

    #endregion Related Data

    public void Update(ProductBaseEntity entity)
    {
        Name = entity.Name;
    }

    private void UpdateTranslations(IEnumerable<ProductParameterTranslationEntity> entities)
    {
        foreach (var translation in entities)
        {
            var result = Translations.FirstOrDefault(x => x.Lang == translation.Lang);
            if (result is null)
            {
                Translations.Add(translation);
                continue;
            }

            result.Update(translation);
        }

        foreach (var translation in Translations.ToList())
        {
            if (!entities.Any(x => x.Lang == translation.Lang))
                Translations.Remove(translation);
        }
    }
}