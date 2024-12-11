using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Core.Errors;
using Product.Core.Extensions;
using Product.Infrastructure;
using Shared.Core.Bases;
using Shared.Core.Dtos;
using System.Net;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record CreateListProductPhotoCommand(IFormFileCollection Files) : IRequest<ResultDto<IEnumerable<string>>>;

internal class CreateListProductPhotoCommandHandler : BaseService, IRequestHandler<CreateListProductPhotoCommand, ResultDto<IEnumerable<string>>>
{
    private readonly ProductMongoDbContext _mongoDbContext;

    public CreateListProductPhotoCommandHandler(ProductMongoDbContext mongoDbContext)
    {
        _mongoDbContext = mongoDbContext;
    }

    public async Task<ResultDto<IEnumerable<string>>> Handle(CreateListProductPhotoCommand request, CancellationToken cancellationToken)
    {
        if (!request.Files.Any())
            return Success<IEnumerable<string>>([]);

        if (request.Files.Any(x => x.Length == 0))
            return Error<IEnumerable<string>>(HttpStatusCode.BadRequest, ExceptionMessage.ProductPhoto001OneOfFilesWasEmpty);

        var productPhotos = request.Files.Select(x => x.ToProductPhotoDocument()).ToList();
        await _mongoDbContext.AddRangeAsync(productPhotos, cancellationToken);

        return Success(productPhotos.Select(x => x.Id));
    }
}