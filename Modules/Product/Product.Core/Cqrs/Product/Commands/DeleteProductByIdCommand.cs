using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.Product.Commands;
public record DeleteProductByIdCommand(Guid Id) : IRequest<ResultDto>;

internal class DeleteProductByIdCommandHandler : BaseService, IRequestHandler<DeleteProductByIdCommand, ResultDto>
{
    private readonly ProductPostgreSqlContext _context;

    public DeleteProductByIdCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
        return Success();
    }
}