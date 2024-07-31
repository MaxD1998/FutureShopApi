using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Core.Extensions;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record CreateListProductPhotoCommand(IFormFileCollection Files) : IRequest;

internal class CreateListProductPhotoCommandHandler : IRequestHandler<CreateListProductPhotoCommand>
{
    private readonly ProductMongoDbContext _mongoDbContext;
    private readonly ProductPostgreSqlContext _productPostgreSqlContext;

    public CreateListProductPhotoCommandHandler(ProductMongoDbContext mongoDbContext, ProductPostgreSqlContext productPostgreSqlContext)
    {
        _mongoDbContext = mongoDbContext;
        _productPostgreSqlContext = productPostgreSqlContext;
    }

    public async Task Handle(CreateListProductPhotoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Files.Any())
            return;

        var productPhotos = request.Files.Select(x => x.ToProductPhotoDocument()).ToList();
        await _mongoDbContext.AddRangeAsync(productPhotos, cancellationToken);
    }
}