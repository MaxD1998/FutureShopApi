using MediatR;
using MongoDB.Driver;
using Product.Domain.Documents;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record DeleteListProductPhotoByIdCommand(IEnumerable<string> Ids) : IRequest;

internal class DeleteListProductPhotoByIdCommandHandler : IRequestHandler<DeleteListProductPhotoByIdCommand>
{
    private readonly ProductMongoDbContext _context;

    public DeleteListProductPhotoByIdCommandHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteListProductPhotoByIdCommand request, CancellationToken cancellationToken)
        => await _context.Set<ProductPhotoDocument>().DeleteManyAsync(x => request.Ids.Contains(x.Id), cancellationToken);
}