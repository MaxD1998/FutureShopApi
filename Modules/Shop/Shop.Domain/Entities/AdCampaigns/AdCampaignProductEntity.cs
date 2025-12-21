using Shared.Domain.Bases;
using Shared.Domain.Exceptions;
using Shared.Domain.Interfaces;
using Shop.Domain.Entities.Products;

namespace Shop.Domain.Entities.AdCampaigns;

public class AdCampaignProductEntity : BaseEntity, IUpdate<AdCampaignProductEntity>, IEntityValidation
{
    public Guid AdCampaignId { get; private set; }

    public Guid ProductId { get; set; }

    #region Related Data

    public AdCampaignEntity AdCampaign { get; private set; }

    public ProductEntity Product { get; private set; }

    #endregion Related Data

    #region Methods

    public void Update(AdCampaignProductEntity entity)
    {
    }

    public void Validate()
    {
        ValidateProductId();
    }

    private void ValidateProductId()
    {
        if (ProductId == Guid.Empty)
            throw new PropertyWasEmptyException(nameof(ProductId));
    }

    #endregion Methods
}