using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Category.Commands;
public record DeleteCategoryByIdCommand(Guid Id) : IRequest;

internal class DeleteCategoryByIdCommandHandler : BaseRequestHandler<ProductContext, DeleteCategoryByIdCommand>
{
    public DeleteCategoryByIdCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task Handle(DeleteCategoryByIdCommand request, CancellationToken cancellationToken)
        => await _context.Set<CategoryEntity>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellationToken);
}