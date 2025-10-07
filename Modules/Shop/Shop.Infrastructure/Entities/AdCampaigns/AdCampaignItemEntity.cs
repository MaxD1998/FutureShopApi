using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;

namespace Shop.Infrastructure.Entities.AdCampaigns;

public class AdCampaignItemEntity : BaseEntity, IUpdate<AdCampaignItemEntity>, IEntityValidation
{
    public Guid AdCampaignId { get; private set; }

    public string FileId { get; set; }

    public string Lang { get; set; }

    public int Position { get; set; }

    #region Related Data

    public AdCampaignEntity AdCampaign { get; private set; }

    #endregion Related Data

    #region Methods

    public void Update(AdCampaignItemEntity entity)
    {
        Position = entity.Position;
    }

    public void Validate()
    {
        ValidateFileId();
        ValidateLang();
        ValidatePosition();
    }

    private void ValidateFileId()
    {
        if (string.IsNullOrWhiteSpace(FileId))
            throw new PropertyWasEmptyException(nameof(FileId));
    }

    private void ValidateLang()
    {
        if (string.IsNullOrWhiteSpace(Lang))
            throw new PropertyWasEmptyException(nameof(Lang));

        if (Lang.Length > StringLengthConst.LongString)
            throw new PropertyWasTooLongException(nameof(Lang), StringLengthConst.LongString);
    }

    private void ValidatePosition()
    {
        if (Position < 0)
            throw new PropertyWasNegativeException(nameof(Position));
    }

    #endregion Methods
}