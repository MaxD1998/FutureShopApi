using Authorization.Core.Dtos.RefreshToken;
using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.Cqrs.RefreshToken.Commands;

public record CreateOrUpdateRefreshTokenEntityByUserIdCommand(Guid UserId, RefreshTokenFormDto Dto) : IRequest<RefreshTokenEntity>;

internal class CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler : IRequestHandler<CreateOrUpdateRefreshTokenEntityByUserIdCommand, RefreshTokenEntity>
{
    private readonly AuthContext _context;

    public CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler(AuthContext context)
    {
        _context = context;
    }

    public async Task<RefreshTokenEntity> Handle(CreateOrUpdateRefreshTokenEntityByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<RefreshTokenEntity>().FirstOrDefaultAsync(x => x.UserId == request.UserId, cancellationToken);

        entity ??= new RefreshTokenEntity();
        entity.Update(request.Dto.ToEntity());

        if (entity.Id == Guid.Empty)
        {
            var newEntity = await _context.Set<RefreshTokenEntity>().AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return newEntity.Entity;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}