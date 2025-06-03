﻿using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Bases;
using Shared.Infrastructure.Interfaces;
using Shop.Domain.Entities;

namespace Shop.Infrastructure.Repositories;

public interface IProductBaseRepository : IBaseRepository<ProductBaseEntity>, IUpdateRepository<ProductBaseEntity>
{
    Task CreateOrUpdateAsync(ProductBaseEntity entity, CancellationToken cancellationToken);

    Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);

    Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken);
}

public class ProductBaseRepository(ShopContext context) : BaseRepository<ShopContext, ProductBaseEntity>(context), IProductBaseRepository
{
    public async Task CreateOrUpdateAsync(ProductBaseEntity eventEntity, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>()
            .FirstOrDefaultAsync(x => x.ExternalId == eventEntity.Id, cancellationToken);

        if (entity is null)
            await _context.Set<ProductBaseEntity>().AddAsync(eventEntity, cancellationToken);
        else
            entity.UpdateEvent(eventEntity);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public Task DeleteByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductBaseEntity>().Where(x => x.ExternalId == externalId).ExecuteDeleteAsync(cancellationToken);

    public Task<Guid?> GetIdByExternalIdAsync(Guid externalId, CancellationToken cancellationToken)
        => _context.Set<ProductBaseEntity>().Where(x => x.ExternalId == externalId).Select(x => (Guid?)x.Id).FirstOrDefaultAsync(cancellationToken);

    public async Task<ProductBaseEntity> UpdateAsync(Guid id, ProductBaseEntity entity, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _context.Set<ProductBaseEntity>()
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (entity == null)
            return null;

        entityToUpdate.Update(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entityToUpdate;
    }
}