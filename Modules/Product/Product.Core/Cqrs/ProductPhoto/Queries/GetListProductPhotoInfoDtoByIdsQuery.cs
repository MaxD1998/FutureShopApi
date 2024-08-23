using MediatR;
using MongoDB.Driver;
using Product.Core.Dtos.ProductPhoto;
using Product.Domain.Documents;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductPhoto.Queries;

public record GetListProductPhotoInfoDtoByIdsQuery(IEnumerable<string> Ids) : IRequest<IEnumerable<ProductPhotoInfoDto>>;

public class GetListProductPhotoInfoDtoByIdsQueryHandler : IRequestHandler<GetListProductPhotoInfoDtoByIdsQuery, IEnumerable<ProductPhotoInfoDto>>
{
    private readonly ProductMongoDbContext _context;

    public GetListProductPhotoInfoDtoByIdsQueryHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductPhotoInfoDto>> Handle(GetListProductPhotoInfoDtoByIdsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _context.Set<ProductPhotoDocument>().Find(x => request.Ids.Contains(x.Id)).Project(x => new { x.Id, x.ContentType, x.Length, x.Name }).ToListAsync(cancellationToken);

        return documents.Select(x => new ProductPhotoInfoDto(x.Id, x.ContentType, x.Length, x.Name));
    }
}