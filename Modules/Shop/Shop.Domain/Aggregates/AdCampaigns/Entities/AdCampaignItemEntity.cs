using Shared.Domain.Bases;
using Shared.Domain.Interfaces;

namespace Shop.Domain.Aggregates.AdCampaigns.Entities;

public class AdCampaignItemEntity : BaseEntity, IUpdate<AdCampaignItemEntity>
{
    public AdCampaignItemEntity(Guid id, string fileId, string lang, int position)
    {
        Id = id;
        SetFileId(fileId);
        SetLang(lang);
        SetPosition(position);
    }

    private AdCampaignItemEntity()
    {
    }

    public Guid AdCampaignId { get; private set; }

    public string FileId { get; private set; }

    public string Lang { get; private set; }

    public int Position { get; private set; }

    #region Setter

    private void SetFileId(string fileId)
    {
        ValidateRequiredProperty(nameof(FileId), fileId);

        FileId = fileId;
    }

    private void SetLang(string lang)
    {
        ValidateRequiredLangStringProperty(nameof(Lang), lang);

        Lang = lang;
    }

    private void SetPosition(int position)
    {
        ValidateNonNegativeProperty(nameof(Position), position);

        Position = position;
    }

    #endregion Setter

    #region Methods

    public void Update(AdCampaignItemEntity entity)
    {
        Position = entity.Position;
    }

    #endregion Methods
}