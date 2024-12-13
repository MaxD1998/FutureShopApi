using MediatR;
using MongoDB.Driver;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using Shop.Domain.Documents;
using Shop.Infrastructure;

namespace Shop.Core.Cqrs.ProductPhoto.Commands;
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