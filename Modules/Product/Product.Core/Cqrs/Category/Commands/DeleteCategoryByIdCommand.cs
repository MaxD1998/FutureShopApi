using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Category.Commands;
public record DeleteCategoryByIdCommand(Guid Id) : IRequest;

internal class DeleteCategoryByIdCommandHandler : IRequestHandler<DeleteCategoryByIdCommand>
{
    private readonly ProductPostgreSqlContext _context;

    public DeleteCategoryByIdCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
}