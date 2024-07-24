using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Authorization.Core.Cqrs.RefreshToken.Commands;
public record DeleteRefreshTokenByUserIdCommand(Guid UserId) : IRequest;

internal class DeleteRefreshTokenByUserIdCommandHandler : IRequestHandler<DeleteRefreshTokenByUserIdCommand>
{
    private readonly AuthContext _context;

    public DeleteRefreshTokenByUserIdCommandHandler(AuthContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteRefreshTokenByUserIdCommand request, CancellationToken cancellationToken)
        => await _context.Set<RefreshTokenEntity>().Where(x => x.UserId == request.UserId).ExecuteDeleteAsync(cancellationToken);
}