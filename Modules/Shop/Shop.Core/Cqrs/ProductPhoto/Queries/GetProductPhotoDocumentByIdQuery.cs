using MediatR;
using MongoDB.Driver;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Documents;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductPhoto.Queries;
public record GetProductPhotoDocumentByIdQuery(string Id) : IRequest<ResultDto<ProductPhotoDocument>>;

internal class GetProductPhotoDocumentByIdQueryHandler : BaseService, IRequestHandler<GetProductPhotoDocumentByIdQuery, ResultDto<ProductPhotoDocument>>
{
    private readonly ProductMongoDbContext _context;

    public GetProductPhotoDocumentByIdQueryHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<ProductPhotoDocument>> Handle(GetProductPhotoDocumentByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductPhotoDocument>().Find(x => x.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
        return Success(result);
    }
}