using MediatR;
using MongoDB.Driver;
using Product.Domain.Documents;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record DeleteProductPhotoByIdCommand(string Id) : IRequest<ResultDto>;

internal class DeleteProductPhotoByIdCommandHandler : BaseService, IRequestHandler<DeleteProductPhotoByIdCommand, ResultDto>
{
    private readonly ProductMongoDbContext _context;

    public DeleteProductPhotoByIdCommandHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task<ResultDto> Handle(DeleteProductPhotoByIdCommand request, CancellationToken cancellationToken)
    {
        await _context.Set<ProductPhotoDocument>().DeleteOneAsync(x => x.Id == request.Id, cancellationToken);
        return Success();
    }
}