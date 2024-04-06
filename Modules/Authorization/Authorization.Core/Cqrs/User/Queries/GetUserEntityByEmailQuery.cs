using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;

namespace Authorization.Core.Cqrs.User.Queries;

public record GetUserEntityByEmailQuery(string Email) : IRequest<UserEntity>;

internal class GetUserEntityByEmailQueryHandler : BaseRequestHandler<AuthContext, GetUserEntityByEmailQuery, UserEntity>
{
    public GetUserEntityByEmailQueryHandler(AuthContext context) : base(context)
    {
    }

    public override async Task<UserEntity> Handle(GetUserEntityByEmailQuery request, CancellationToken cancellationToken)
        => await _context.Set<UserEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email);
}