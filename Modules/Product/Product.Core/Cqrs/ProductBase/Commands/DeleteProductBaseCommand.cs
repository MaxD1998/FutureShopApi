using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductBase.Commands;
public record DeleteProductBaseCommand(Guid Id) : IRequest;

internal class DeleteProductBaseCommandHandler : IRequestHandler<DeleteProductBaseCommand>
{
    private readonly ProductPostgreSqlContext _context;

    public DeleteProductBaseCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductBaseCommand request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
}