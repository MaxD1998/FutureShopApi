using File.Core.Helpers;
using File.Domain.Entities;
using File.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace File.Core.Cqrs.ProductPhoto.Commands;
public record CreateProductPhotoCommand(Guid ProductId, IFormFile File) : IRequest;

internal class CreateProductPhotoCommandHandler : IRequestHandler<CreateProductPhotoCommand>
{
    private readonly FileContext _context;

    public CreateProductPhotoCommandHandler(FileContext context)
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