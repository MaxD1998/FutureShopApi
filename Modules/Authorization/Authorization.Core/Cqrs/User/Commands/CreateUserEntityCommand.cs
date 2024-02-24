using Authorization.Core.Dtos.User;
using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Authorization.Core.Cqrs.User.Commands;
public record CreateUserEntityCommand(UserInputDto Dto) : IRequest<UserEntity>;

internal class CreateUserEntityCommandHandler : BaseRequestHandler<AuthContext, CreateUserEntityCommand, UserEntity>
{
    public CreateUserEntityCommandHandler(AuthContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<UserEntity> Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _context.Set<UserEntity>().AnyAsync(x => x.Email == request.Dto.Email);

        if (isExist)
            throw new ConflictException(ExceptionMessage.RecordAlreadyExists);

        return await Create<UserEntity>(request.Dto);
    }
}