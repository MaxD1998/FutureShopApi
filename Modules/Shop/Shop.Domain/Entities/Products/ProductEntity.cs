using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shared.Shared.Constants;
using Shop.Domain.DomainLogics;
using Shop.Domain.Entities.AdCampaigns;
using Shop.Domain.Entities.Baskets;
using Shop.Domain.Entities.ProductBases;
using Shop.Domain.Entities.Promotions;
using Shop.Domain.Entities.PurchaseLists;
using Shared.Domain.Extensions;

namespace Shop.Domain.Entities.Products;

public class ProductEntity : BaseExternalEntity, IUpdate<ProductEntity>, IUpdateEvent<ProductEntity>, IEntityValidation
{
    public bool IsActive { get; set; }

    public string Name { get; set; }

    public Guid ProductBaseId { get; set; }

    public bool WasActive { get; set; }

    #region Related Data

    public ICollection<AdCampaignProductEntity> AdCampaignProducts { get; private set; } = [];

    public ICollection<BasketItemEntity> BasketItems { get; private set; } = [];

    public ICollection<PriceEntity> Prices { get; set; } = [];

    public ProductBaseEntity ProductBase { get; private set; }

    public ICollection<ProductParameterValueEntity> ProductParameterValues { get; set; } = [];

    public ICollection<ProductPhotoEntity> ProductPhotos { get; set; } = [];

    public ICollection<ProductReviewEntity> ProductReviews { get; set; } = [];

    public ICollection<PromotionProductEntity> PromotionProducts { get; private set; } = [];

    public ICollection<PurchaseListItemEntity> PurchaseListItems { get; private set; } = [];

    public ICollection<ProductTranslationEntity> Translations { get; set; } = [];

    #endregion Related Data

    #region Methods

    public void Update(ProductEntity entity)
    {
        IsActive = entity.IsActive;
        Name = entity.Name;

        if (!WasActive && entity.IsActive)
            WasActive = entity.IsActive;

        ProductParameterValues.UpdateEntities(entity.ProductParameterValues);
        Translations.UpdateEntities(entity.Translations);

        UpdatePrices(Prices, entity.Prices);
    }

    public void UpdateEvent(ProductEntity entity)
    {
        Name = entity.Name;
        ProductPhotos.UpdateEventEntities(entity.ProductPhotos);
    }

    public void Validate()
    {
        ValidateName();
        ValidateProductBaseId();

        ProductParameterValues.ValidateEntities();
        ProductPhotos.ValidateEntities();
        Translations.ValidateEntities();
    }

    #region Validation

    private void ValidateName()
    {
        var length = StringLengthConst.LongString;

        if (string.IsNullOrWhiteSpace(Name))
            throw new PropertyWasEmptyException(nameof(Name));

        if (Name.Length > length)
            throw new PropertyWasTooLongException(nameof(Name), length);
    }

    private void ValidateProductBaseId()
    {
        if (ProductBaseId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(ProductBaseId));
    }

    #endregion Validation

    private void UpdatePrices(ICollection<PriceEntity> entities, ICollection<PriceEntity> updateEntities)
    {
        var utcNow = DateTime.UtcNow;

        foreach (var updateEntity in updateEntities.Except(entities))
        {
            if (entities.Count == 0)
            {
                updateEntity.Start = null;
                updateEntity.End = null;

                entities.Add(updateEntity);
                continue;
            }

            var isExist = entities.Any(x => x.Id == updateEntity.Id && x.Id != Guid.Empty);

            if (!isExist)
            {
                PriceDomainLogic.Add(entities, updateEntity, utcNow, WasActive);
                continue;
            }

            PriceDomainLogic.Update(entities, updateEntity, utcNow, WasActive);
        }

        PriceDomainLogic.Remove(entities, updateEntities, utcNow, WasActive);
    }

    #endregion Methods
}