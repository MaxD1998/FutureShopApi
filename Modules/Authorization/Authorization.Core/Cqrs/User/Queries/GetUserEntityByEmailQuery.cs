using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using AutoMapper;
using MediatR;
using Shared.Core.Bases;

namespace Authorization.Core.Cqrs.User.Queries;

public record GetUserEntityByEmailQuery(string Email) : IRequest<UserEntity>;

internal class GetUserEntityByEmailQueryHandler : BaseRequestHandler<AuthContext, GetUserEntityByEmailQuery, UserEntity>
{
    public GetUserEntityByEmailQueryHandler(AuthContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<UserEntity> Handle(GetUserEntityByEmailQuery request, CancellationToken cancellationToken)
        => await GetAsync<UserEntity>(x => x.Email == request.Email);
}