using Authorization.Core.Dtos.User;
using Authorization.Domain.Entities;
using Authorization.Inrfrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shared.Core.Errors;
using System.Net;

namespace Authorization.Core.Cqrs.User.Commands;
public record CreateUserEntityCommand(UserFormDto Dto) : IRequest<ResultDto<UserEntity>>;

internal class CreateUserEntityCommandHandler : BaseService, IRequestHandler<CreateUserEntityCommand, ResultDto<UserEntity>>
{
    private readonly AuthContext _context;

    public CreateUserEntityCommandHandler(AuthContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<UserEntity>> Handle(CreateUserEntityCommand request, CancellationToken cancellationToken)
    {
        var isExist = await _context.Set<UserEntity>().AnyAsync(x => x.Email == request.Dto.Email, cancellationToken);

        if (isExist)
            return Error<UserEntity>(HttpStatusCode.Conflict, CommonExceptionMessage.C006RecordAlreadyExists);

        var result = await _context.Set<UserEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Success(result.Entity);
    }
}