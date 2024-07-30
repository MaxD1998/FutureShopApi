using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Core.Helpers;
using Product.Domain.Documents;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record CreateListProductPhotoCommand(IEnumerable<IFormFile> Files) : IRequest;

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
        foreach (var file in request.Files)
        {
            var document = new ProductPhotoDocument()
            {
                ContentType = file.ContentType,
                Data = ConversionHelper.ToByte(file),
                Name = file.FileName,
            };

            await _mongoDbContext.AddAsync(document, cancellationToken);
        }
    }
}