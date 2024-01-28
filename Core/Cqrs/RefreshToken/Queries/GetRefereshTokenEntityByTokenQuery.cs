using AutoMapper;
using Core.Bases;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Core.Cqrs.RefreshToken.Queries;

public record GetRefereshTokenEntityByTokenQuery(Guid Token) : IRequest<RefreshTokenEntity>;

internal class GetRefereshTokenEntityByTokenQueryHandler : BaseRequestHandler<GetRefereshTokenEntityByTokenQuery, RefreshTokenEntity>
{
    public GetRefereshTokenEntityByTokenQueryHandler(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<RefreshTokenEntity> Handle(GetRefereshTokenEntityByTokenQuery request, CancellationToken cancellationToken)
        => await GetAsync<RefreshTokenEntity>(x => x.Token == request.Token);
}