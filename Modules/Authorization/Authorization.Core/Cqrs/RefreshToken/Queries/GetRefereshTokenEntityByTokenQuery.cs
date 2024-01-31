using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using AutoMapper;
using MediatR;
using Shared.Core.Bases;

namespace Authorization.Core.Cqrs.RefreshToken.Queries;

public record GetRefereshTokenEntityByTokenQuery(Guid Token) : IRequest<RefreshTokenEntity>;

internal class GetRefereshTokenEntityByTokenQueryHandler : BaseRequestHandler<AuthContext, GetRefereshTokenEntityByTokenQuery, RefreshTokenEntity>
{
    public GetRefereshTokenEntityByTokenQueryHandler(AuthContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<RefreshTokenEntity> Handle(GetRefereshTokenEntityByTokenQuery request, CancellationToken cancellationToken)
        => await GetAsync<RefreshTokenEntity>(x => x.Token == request.Token);
}