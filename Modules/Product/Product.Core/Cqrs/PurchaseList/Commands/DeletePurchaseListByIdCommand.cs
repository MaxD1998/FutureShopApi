using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.PurchaseList.Commands;
public record DeletePurchaseListByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeletePurchaseListByIdCommandHandler : BaseService, IRequestHandler<DeletePurchaseListByIdCommand, ResultDto>
{
    private readonly ProductPostgreSqlContext _context;

    public DeletePurchaseListByIdCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeletePurchaseListByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<PurchaseListEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return Success();
    }
}