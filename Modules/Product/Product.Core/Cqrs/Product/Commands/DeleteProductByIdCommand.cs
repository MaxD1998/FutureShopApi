using MediatR;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Product.Commands;
public record DeleteProductByIdCommand(Guid Id) : IRequest;

internal class DeleteProductByIdCommandHandler : BaseRequestHandler<ProductContext, DeleteProductByIdCommand>
{
    public DeleteProductByIdCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
        => await DeleteByIdAsync<ProductEntity>(request.Id, cancellationToken);
}