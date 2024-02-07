using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Shared.Core.Bases;

namespace Authorization.Core.Cqrs.RefreshToken.Commands;
public record DeleteRefreshTokenByUserIdCommand(Guid UserId) : IRequest;

internal class DeleteRefreshTokenByUserIdCommandHandler : BaseRequestHandler<AuthContext, DeleteRefreshTokenByUserIdCommand>
{
    public DeleteRefreshTokenByUserIdCommandHandler(AuthContext context) : base(context)
    {
    }

    public override async Task Handle(DeleteRefreshTokenByUserIdCommand request, CancellationToken cancellationToken)
        => await DeleteAsync<RefreshTokenEntity>(x => x.UserId == request.UserId);
}