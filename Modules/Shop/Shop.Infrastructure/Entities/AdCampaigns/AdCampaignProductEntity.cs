using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Exceptions;
using Shared.Infrastructure.Interfaces;
using Shop.Infrastructure.Entities.Products;

namespace Shop.Infrastructure.Entities.AdCampaigns;

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