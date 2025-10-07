namespace Shared.Infrastructure.Interfaces;

public interface IUpdate<in TEntity> where TEntity : IEntity
{
    void Update(TEntity entity);
}