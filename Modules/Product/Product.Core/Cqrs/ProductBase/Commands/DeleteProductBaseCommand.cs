using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.ProductBase.Commands;
public record DeleteProductBaseCommand(Guid Id) : IRequest;

internal class DeleteProductBaseCommandHandler : BaseRequestHandler<ProductContext, DeleteProductBaseCommand>
{
    public DeleteProductBaseCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task Handle(DeleteProductBaseCommand request, CancellationToken cancellationToken)
        => await _context.Set<ProductBaseEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
}