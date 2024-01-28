using AutoMapper;
using Core.Bases;
using Core.Dtos.RefreshToken;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Core.Cqrs.RefreshToken.Commands;

public record CreateOrUpdateRefreshTokenEntityByUserIdCommand(Guid UserId, RefreshTokenInputDto Dto) : IRequest<RefreshTokenEntity>;

internal class CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler : BaseRequestHandler<CreateOrUpdateRefreshTokenEntityByUserIdCommand, RefreshTokenEntity>
{
    public CreateOrUpdateRefreshTokenEntityByUserIdCommandHandler(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<RefreshTokenEntity> Handle(CreateOrUpdateRefreshTokenEntityByUserIdCommand request, CancellationToken cancellationToken)
        => await CreateOrUpdateAsync<RefreshTokenEntity>(request.Dto, x => x.UserId == request.UserId);
}