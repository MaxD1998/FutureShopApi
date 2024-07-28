using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Product.Commands;
public record DeleteProductByIdCommand(Guid Id) : IRequest;

internal class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand>
{
    private readonly ProductPostgreSqlContext _context;

    public DeleteProductByIdCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        => await _context.Set<ProductEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
}