namespace Shared.Infrastructure.Interfaces;

public interface IUpdateEvent<TEntity> where TEntity : IEntity
{
    void UpdateEvent(TEntity entity);
}