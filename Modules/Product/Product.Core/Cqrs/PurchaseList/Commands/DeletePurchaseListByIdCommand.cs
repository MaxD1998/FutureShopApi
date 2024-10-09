using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.PurchaseList.Commands;
public record DeletePurchaseListByIdCommand(Guid Id) : IRequest;

internal class DeletePurchaseListByIdCommandHandler : IRequestHandler<DeletePurchaseListByIdCommand>
{
    private readonly ProductPostgreSqlContext _context;

    public DeletePurchaseListByIdCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task Handle(DeletePurchaseListByIdCommand request, CancellationToken cancellationToken)
        => await _context.Set<PurchaseListEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
}