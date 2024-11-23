using Authorization.Core.Dtos.User;
using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Authorization.Core.Cqrs.User.Commands;
public record CreateUserEntityCommand(UserFormDto Dto) : IRequest<UserEntity>;

internal class CreateUserEntityCommandHandler : IRequestHandler<CreateUserEntityCommand, UserEntity>
{
    private readonly AuthContext _context;

    public CreateUserEntityCommandHandler(AuthContext context)
    {
        _context = context;
    }

    public async Task<UserEntity> Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _context.Set<UserEntity>().AnyAsync(x => x.Email == request.Dto.Email, cancellationToken);

        if (isExist)
            throw new ConflictException(CommonExceptionMessage.C006RecordAlreadyExists);

        var result = await _context.Set<UserEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return result.Entity;
    }
}