using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Core.Helpers;
using Product.Domain.Documents;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record CreateProductPhotoCommand(Guid ProductId, IFormFile File) : IRequest;

internal class CreateProductPhotoCommandHandler : IRequestHandler<CreateProductPhotoCommand>
{
    private readonly ProductMongoDbContext _context;

    public CreateProductPhotoCommandHandler(ProductMongoDbContext context)
    {
        _context = context;
    }

    public async Task Handle(CreateProductPhotoCommand request, CancellationToken cancellationToken)
    {
        var document = new ProductPhotoDocument()
        {
            Data = ConversionHelper.ToByte(request.File),
            Name = request.File.FileName,
            ProductId = request.ProductId,
        };

        await _context.AddAsync(document, cancellationToken);
    }
}