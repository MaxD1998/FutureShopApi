namespace Product.Core.Dtos.CategoryTranslation;

public class CategoryTranslationInputDto
{
    public Guid CategoryId { get; set; }

    public string Lang { get; set; }

    public string Translation { get; set; }
}