using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.Cqrs.RefreshToken.Queries;

public record GetRefereshTokenEntityByTokenQuery(Guid Token) : IRequest<RefreshTokenEntity>;

internal class GetRefereshTokenEntityByTokenQueryHandler : IRequestHandler<GetRefereshTokenEntityByTokenQuery, RefreshTokenEntity>
{
    private readonly AuthContext _context;

    public GetRefereshTokenEntityByTokenQueryHandler(AuthContext context)
    {
        _context = context;
    }

    public async Task<RefreshTokenEntity> Handle(GetRefereshTokenEntityByTokenQuery request, CancellationToken cancellationToken)
        => await _context.Set<RefreshTokenEntity>()
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);
}