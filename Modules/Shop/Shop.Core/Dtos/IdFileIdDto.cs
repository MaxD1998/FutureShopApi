using Shop.Infrastructure.Persistence.Entities.AdCampaigns;
using System.Linq.Expressions;

namespace Shop.Core.Dtos;

public class IdFileIdDto
{
    public string FileId { get; set; }

    public Guid Id { get; set; }

    public static Expression<Func<AdCampaignEntity, IdFileIdDto>> MapFromAdCampaign(string lang) => entity => new IdFileIdDto
    {
        Id = entity.Id,
        FileId = entity.AdCampaignItems.AsQueryable().Where(x => x.Lang == lang).Select(x => x.FileId).FirstOrDefault(),
    };
}