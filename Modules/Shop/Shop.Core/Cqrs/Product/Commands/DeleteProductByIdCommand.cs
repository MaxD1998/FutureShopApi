using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.Product.Commands;
public record DeleteProductByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteProductByIdCommandHandler : BaseService, IRequestHandler<DeleteProductByIdCommand, ResultDto>
{
    private readonly ShopPostgreSqlContext _context;

    public DeleteProductByIdCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductEntity>().Where(x => x.ExternalId == request.Id).ExecuteDeleteAsync(cancellationToken);

        return Success();
    }
}