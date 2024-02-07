using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Interfaces;
using Shared.Domain.Bases;
using Shared.Infrastructure.Bases;
using System.Linq.Expressions;

namespace Shared.Core.Bases;

public abstract class BaseRequestHandler<TContext, TRequest> : IRequestHandler<TRequest>
    where TContext : BaseContext
    where TRequest : IRequest
{
    protected readonly TContext _context;

    public BaseRequestHandler(TContext context)
    {
        _context = context;
    }

    public abstract Task Handle(TRequest request, CancellationToken cancellationToken);

    protected async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : BaseEntity
        => await _context.Set<TEntity>().Where(predicate).ExecuteDeleteAsync();
}

public abstract class BaseRequestHandler<TContext, TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TContext : BaseContext
    where TRequest : IRequest<TResponse>
{
    protected readonly TContext _context;
    protected readonly IMapper _mapper;

    public BaseRequestHandler(TContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected async Task<TEntity> CreateOrUpdateAsync<TEntity>(IDto dto, Expression<Func<TEntity, bool>> predicate)
        where TEntity : BaseEntity, new()
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

        entity ??= new TEntity();

        _mapper.Map(dto, entity);

        if (entity.Id == Guid.Empty)
        {
            var newEntity = await _context.Set<TEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return newEntity.Entity;
        }

        await _context.SaveChangesAsync();

        return entity;
    }

    protected async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> condition)
        where TEntity : BaseEntity
        => await _context.Set<TEntity>()
            .AsNoTracking()
            .Where(condition)
            .ProjectTo<TEntity>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
}