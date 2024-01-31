using Authorization.Core.Dtos.RefreshToken;
using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using AutoMapper;
using MediatR;
using Shared.Core.Bases;

namespace Authorization.Core.Cqrs.RefreshToken.Commands;

public record CreateOrUpdateRefreshTokenEntityByUserIdCommand(Guid UserId, RefreshTokenInputDto Dto) : IRequest<RefreshTokenEntity>;

internal class CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler : BaseRequestHandler<AuthContext, CreateOrUpdateRefreshTokenEntityByUserIdCommand, RefreshTokenEntity>
{
    public CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler(AuthContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<RefreshTokenEntity> Handle(CreateOrUpdateRefreshTokenEntityByUserIdCommand request, CancellationToken cancellationToken)
        => await CreateOrUpdateAsync<RefreshTokenEntity>(request.Dto, x => x.UserId == request.UserId);
}