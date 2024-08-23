using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Core.Errors;
using Product.Core.Extensions;
using Product.Infrastructure;
using Shared.Core.Exceptions;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record CreateListProductPhotoCommand(IFormFileCollection Files) : IRequest<IEnumerable<string>>;

internal class CreateListProductPhotoCommandHandler : IRequestHandler<CreateListProductPhotoCommand, IEnumerable<string>>
{
    private readonly ProductMongoDbContext _mongoDbContext;
    private readonly ProductPostgreSqlContext _productPostgreSqlContext;

    public CreateListProductPhotoCommandHandler(ProductMongoDbContext mongoDbContext, ProductPostgreSqlContext productPostgreSqlContext)
    {
        _mongoDbContext = mongoDbContext;
        _productPostgreSqlContext = productPostgreSqlContext;
    }

    public async Task<IEnumerable<string>> Handle(CreateListProductPhotoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Files.Any())
            return [];

        if (request.Files.Any(x => x.Length == 0))
            throw new BadRequestException(ExceptionMessage.ProductPhoto001OoneOfFilesWasEmpty);

        var productPhotos = request.Files.Select(x => x.ToProductPhotoDocument()).ToList();
        await _mongoDbContext.AddRangeAsync(productPhotos, cancellationToken);

        return productPhotos.Select(x => x.Id);
    }
}