using MediatR;
using MongoDB.Driver;
using Product.Domain.Documents;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductPhoto.Queries;
public record GetProductPhotoDocumentByIdQuery(string Id) : IRequest<ProductPhotoDocument>;

internal class GetProductPhotoDocumentByIdQueryHandler : IRequestHandler<GetProductPhotoDocumentByIdQuery, ProductPhotoDocument>
{
    private readonly ProductMongoDbContext _context;

    public GetProductPhotoDocumentByIdQueryHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task<ProductPhotoDocument> Handle(GetProductPhotoDocumentByIdQuery request, CancellationToken cancellationToken)
        => await _context.Set<ProductPhotoDocument>().Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
}