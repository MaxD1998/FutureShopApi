using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Entities;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductBase.Commands;
public record DeleteProductBaseCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteProductBaseCommandHandler : BaseService, IRequestHandler<DeleteProductBaseCommand, ResultDto>
{
    private readonly ShopContext _context;

    public DeleteProductBaseCommandHandler(ShopContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteProductBaseCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductBaseEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return Success();
    }
}