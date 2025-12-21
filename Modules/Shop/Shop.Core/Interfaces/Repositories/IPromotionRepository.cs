using Shared.Core.Interfaces;
using Shop.Domain.Entities.Promotions;

namespace Shop.Core.Interfaces.Repositories;

public interface IPromotionRepository : IBaseRepository<PromotionEntity>, IUpdateRepository<PromotionEntity>
{
    Task<List<PromotionEntity>> GetActivePromotionsAsync(List<string> codes, CancellationToken cancellationToken);
}