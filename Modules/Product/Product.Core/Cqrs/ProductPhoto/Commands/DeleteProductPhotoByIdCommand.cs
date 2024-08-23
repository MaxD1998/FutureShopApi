using MediatR;
using MongoDB.Driver;
using Product.Domain.Documents;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record DeleteProductPhotoByIdCommand(string Id) : IRequest;

internal class DeleteProductPhotoByIdCommandHandler : IRequestHandler<DeleteProductPhotoByIdCommand>
{
    private readonly ProductMongoDbContext _context;

    public DeleteProductPhotoByIdCommandHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public Task Handle(DeleteProductPhotoByIdCommand request, CancellationToken cancellationToken)
        => _context.Set<ProductPhotoDocument>().DeleteOneAsync(x => x.Id == request.Id, cancellationToken);
}