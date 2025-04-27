using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.PurchaseList.Commands;
public record DeletePurchaseListByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeletePurchaseListByIdCommandHandler : BaseService, IRequestHandler<DeletePurchaseListByIdCommand, ResultDto>
{
    private readonly ShopPostgreSqlContext _context;

    public DeletePurchaseListByIdCommandHandler(ShopPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeletePurchaseListByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<PurchaseListEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return Success();
    }
}