using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Infrastructure.Constants;
using Shared.Infrastructure.Extensions;
using Shop.Infrastructure.Persistence.Entities.AdCampaigns;
using Shop.Infrastructure.Persistence.Enums;
using Shop.Infrastructure.Persistence.Exceptions.AdCampaigns;
using System.Text.Json;

namespace Shop.Infrastructure.Persistence.Entities.Promotions;

public class PromotionEntity : BaseEntity, IUpdate<PromotionEntity>, IEntityValidation
{
    public Guid? AdCampaignId { get; set; }

    public string Code { get; set; }

    public DateTime End { get; set; }

    public bool IsActive { get; set; }

    public string Name { get; set; }

    public DateTime Start { get; set; }

    public PromotionType Type { get; set; }

    public JsonDocument Value { get; set; }

    #region Related Data

    public AdCampaignEntity AdCampaign { get; private set; }

    public ICollection<PromotionProductEntity> PromotionProducts { get; set; }

    #endregion Related Data

    #region Methods

    public void Update(PromotionEntity entity)
    {
        AdCampaignId = entity.AdCampaignId;
        Code = entity.Code;
        End = entity.End;
        IsActive = entity.IsActive;
        Name = entity.Name;
        Start = entity.Start;
        Type = entity.Type;
        Value = entity.Value;

        PromotionProducts.UpdateEntities(entity.PromotionProducts);
    }

    public void Validate()
    {
        ValidateCode();
        ValidateIsActive();
        ValidateName();
        ValidateStartEnd();

        PromotionProducts.ValidateEntities();
    }

    private void ValidateCode()
    {
        if (!string.IsNullOrWhiteSpace(Code))
        {
            var length = StringLengthConst.ShortString;

            if (Code.Length > length)
                throw new PropertyWasTooLongException(nameof(Code), length);
        }
    }

    private void ValidateIsActive()
    {
        var hasItems = PromotionProducts.Count != 0;

        if (IsActive && !hasItems)
            throw new AdCampaignActivationRequiresItemsException();
    }

    private void ValidateName()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new PropertyWasEmptyException(nameof(Name));

        var length = StringLengthConst.LongString;

        if (Name.Length > length)
            throw new PropertyWasTooLongException(nameof(Name), length);
    }

    private void ValidateStartEnd()
    {
        if (Start > End)
            throw new InvalidDateRangeException(Start, End);
    }

    #endregion Methods
}