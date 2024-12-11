using MediatR;
using MongoDB.Driver;
using Product.Domain.Documents;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record DeleteListProductPhotoByIdCommand(IEnumerable<string> Ids) : IRequest<ResultDto>;

internal class DeleteListProductPhotoByIdCommandHandler : BaseService, IRequestHandler<DeleteListProductPhotoByIdCommand, ResultDto>
{
    private readonly ProductMongoDbContext _context;

    public DeleteListProductPhotoByIdCommandHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteListProductPhotoByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductPhotoDocument>().DeleteManyAsync(x => request.Ids.Contains(x.Id), cancellationToken);
        return Success();
    }
}