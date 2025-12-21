using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shop.Infrastructure.Persistence.Entities.Promotions;
using Shop.Infrastructure.Persistence.Enums;
using Shop.Infrastructure.Persistence.Exceptions.AdCampaigns;

namespace Shop.Infrastructure.Persistence.Entities.AdCampaigns;

public class AdCampaignEntity : BaseEntity, IUpdate<AdCampaignEntity>, IEntityValidation
{
    public DateTime End { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    public AdCampaignType Type { get; set; }

    #region Related Data

    public ICollection<AdCampaignItemEntity> AdCampaignItems { get; set; } = [];

    public ICollection<AdCampaignProductEntity> AdCampaignProducts { get; set; } = [];

    public PromotionEntity Promotion { get; set; }

    #endregion Related Data

    #region Methods

    public void Update(AdCampaignEntity entity)
    {
        End = entity.End;
        IsActive = entity.IsActive;
        Name = entity.Name;
        Start = entity.Start;

        Promotion = entity.Promotion;

        AdCampaignItems.UpdateEntities(entity.AdCampaignItems);
        AdCampaignProducts.UpdateEntities(entity.AdCampaignProducts);
    }

    public void Validate()
    {
        ValidateIsActive();
        ValidateName();
        ValidateStartEnd();

        ValidateAdCampaignItems();

        AdCampaignItems.ValidateEntities();
        AdCampaignProducts.ValidateEntities();
    }

    private void ValidateAdCampaignItems()
    {
        if (AdCampaignItems.Count > 1)
        {
            var isDuplicated = AdCampaignItems.GroupBy(x => x.Lang).Any(g => g.Count() > 1);
            if (isDuplicated)
                throw new AdCampaignItemsMustBeUniqueException();
        }
    }

    private void ValidateIsActive()
    {
        var hasItems = AdCampaignItems.Count != 0;

        if (IsActive && !hasItems)
            throw new AdCampaignActivationRequiresItemsException();
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new PropertyWasEmptyException(nameof(Name));

        if (Name.Length > StringLengthConst.LongString)
            throw new PropertyWasTooLongException(nameof(Name), StringLengthConst.LongString);
    }

    private void ValidateStartEnd()
    {
        if (Start > End)
            throw new InvalidDateRangeException(Start, End);
    }

    #endregion Methods
}