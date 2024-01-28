using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Bases;
using System.Linq.Expressions;

namespace Core.Bases;

public abstract class BaseRequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected readonly DataContext _context;
    protected readonly IMapper _mapper;

    public BaseRequestHandler(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    protected async Task<TEntity> CreateOrUpdateAsync<TEntity>(IDto dto, Expression<Func<TEntity, bool>> predicate)
        where TEntity : BaseEntity
    {
        var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(predicate);

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