using Shop.Domain.Entities;
using System.Linq.Expressions;

namespace Shop.Core.Dtos.AdCampaignItem;

public class AdCampaignItemFormDto
{
    public string FileId { get; set; }

    public Guid? Id { get; set; }

    public string Lang { get; set; }

    public static Expression<Func<AdCampaignItemEntity, AdCampaignItemFormDto>> Map() => entity => new()
    {
        FileId = entity.FileId,
        Id = entity.Id,
        Lang = entity.Lang,
    };

    public AdCampaignItemEntity ToEntity(int index) => new()
    {
        Id = Id ?? Guid.Empty,
        FileId = FileId,
        Lang = Lang,
        Position = index,
    };
}