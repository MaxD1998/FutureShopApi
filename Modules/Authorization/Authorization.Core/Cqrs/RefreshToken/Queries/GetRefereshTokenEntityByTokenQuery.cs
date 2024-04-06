using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;

namespace Authorization.Core.Cqrs.RefreshToken.Queries;

public record GetRefereshTokenEntityByTokenQuery(Guid Token) : IRequest<RefreshTokenEntity>;

internal class GetRefereshTokenEntityByTokenQueryHandler : BaseRequestHandler<AuthContext, GetRefereshTokenEntityByTokenQuery, RefreshTokenEntity>
{
    public GetRefereshTokenEntityByTokenQueryHandler(AuthContext context) : base(context)
    {
    }

    public override async Task<RefreshTokenEntity> Handle(GetRefereshTokenEntityByTokenQuery request, CancellationToken cancellationToken)
        => await _context.Set<RefreshTokenEntity>()
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == request.Token);
}