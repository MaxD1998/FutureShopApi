using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductBase.Commands;
public record DeleteProductBaseCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteProductBaseCommandHandler : BaseService, IRequestHandler<DeleteProductBaseCommand, ResultDto>
{
    private readonly ProductPostgreSqlContext _context;

    public DeleteProductBaseCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteProductBaseCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductBaseEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return Success();
    }
}