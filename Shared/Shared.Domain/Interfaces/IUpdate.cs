namespace Shared.Domain.Interfaces;

public interface IUpdate<in TEntity> where TEntity : IEntity
{
    void Update(TEntity entity);
}