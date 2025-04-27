using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Commands;

public record DeleteProductBaseByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteProductBaseByIdCommandHandler : BaseService, IRequestHandler<DeleteProductBaseByIdCommand, ResultDto>
{
    private readonly ShopPostgreSqlContext _context;

    public DeleteProductBaseByIdCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteProductBaseByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductBaseEntity>().Where(x => x.ExternalId == request.Id).ExecuteDeleteAsync(cancellationToken);

        return Success();
    }
}