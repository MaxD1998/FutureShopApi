using AutoMapper;
using Core.Bases;
using Domain.Entities;
using Infrastructure;
using MediatR;

namespace Core.Cqrs.User.Queries;

public record GetUserEntityByEmailQuery(string Email) : IRequest<UserEntity>;

internal class GetUserEntityByEmailQueryHandler : BaseRequestHandler<GetUserEntityByEmailQuery, UserEntity>
{
    public GetUserEntityByEmailQueryHandler(DataContext context, IMapper mapper) : base(context, mapper)
    {
    }

    public override async Task<UserEntity> Handle(GetUserEntityByEmailQuery request, CancellationToken cancellationToken)
        => await GetAsync<UserEntity>(x => x.Email == request.Email);
}