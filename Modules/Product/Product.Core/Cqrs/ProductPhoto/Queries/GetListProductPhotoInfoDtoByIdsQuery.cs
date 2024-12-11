using MediatR;
using MongoDB.Driver;
using Product.Core.Dtos.ProductPhoto;
using Product.Domain.Documents;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductPhoto.Queries;

public record GetListProductPhotoInfoDtoByIdsQuery(IEnumerable<string> Ids) : IRequest<ResultDto<IEnumerable<ProductPhotoInfoDto>>>;

public class GetListProductPhotoInfoDtoByIdsQueryHandler : BaseService, IRequestHandler<GetListProductPhotoInfoDtoByIdsQuery, ResultDto<IEnumerable<ProductPhotoInfoDto>>>
{
    private readonly ProductMongoDbContext _context;

    public GetListProductPhotoInfoDtoByIdsQueryHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto<IEnumerable<ProductPhotoInfoDto>>> Handle(GetListProductPhotoInfoDtoByIdsQuery request, CancellationToken cancellationToken)
    {
        var documents = await _context.Set<ProductPhotoDocument>().Find(x => request.Ids.Contains(x.Id)).Project(x => new { x.Id, x.ContentType, x.Length, x.Name }).ToListAsync(cancellationToken);

        return Success(documents.Select(x => new ProductPhotoInfoDto(x.Id, x.ContentType, x.Length, x.Name)));
    }
}