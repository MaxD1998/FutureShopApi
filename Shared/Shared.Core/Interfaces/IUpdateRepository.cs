using Shared.Domain.Bases;

namespace Shared.Core.Interfaces;

public interface IUpdateRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> UpdateAsync(Guid id, TEntity entity, CancellationToken cancellationToken);
}