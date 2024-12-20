﻿namespace Shared.Domain.Interfaces;

public interface IUpdate<TEntity> where TEntity : IEntity
{
    void Update(TEntity entity);
}