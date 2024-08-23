using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.Cqrs.User.Queries;

public record GetUserEntityByEmailQuery(string Email) : IRequest<UserEntity>;

internal class GetUserEntityByEmailQueryHandler : IRequestHandler<GetUserEntityByEmailQuery, UserEntity>
{
    private readonly AuthContext _context;

    public GetUserEntityByEmailQueryHandler(AuthContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> Handle(GetUserEntityByEmailQuery request, CancellationToken cancellationToken)
        => await _context.Set<UserEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
}