using Authorization.Core.Dtos.RefreshToken;
using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;

namespace Authorization.Core.Cqrs.RefreshToken.Commands;

public record CreateOrUpdateRefreshTokenEntityByUserIdCommand(Guid UserId, RefreshTokenInputDto Dto) : IRequest<RefreshTokenEntity>;

internal class CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler : BaseRequestHandler<AuthContext, CreateOrUpdateRefreshTokenEntityByUserIdCommand, RefreshTokenEntity>
{
    public CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler(AuthContext context) : base(context)
    {
    }

    public override async Task<RefreshTokenEntity> Handle(CreateOrUpdateRefreshTokenEntityByUserIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<RefreshTokenEntity>().FirstOrDefaultAsync(x => x.UserId == request.UserId);

        entity ??= new RefreshTokenEntity();
        entity.Update(request.Dto.ToEntity());

        if (entity.Id == Guid.Empty)
        {
            var newEntity = await _context.Set<RefreshTokenEntity>().AddAsync(entity);

            await _context.SaveChangesAsync();

            return newEntity.Entity;
        }

        await _context.SaveChangesAsync();

        return entity;
    }
}