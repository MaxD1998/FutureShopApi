using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Product.Core.Helpers;
using Product.Domain.Documents;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Errors;
using Shared.Core.Exceptions;

namespace Product.Core.Cqrs.ProductPhoto.Commands;
public record CreateProductPhotoCommand(Guid ProductId, IFormFile File) : IRequest;

internal class CreateProductPhotoCommandHandler : IRequestHandler<CreateProductPhotoCommand>
{
    private readonly ProductMongoDbContext _mongoDbContext;
    private readonly ProductPostgreSqlContext _productPostgreSqlContext;

    public CreateProductPhotoCommandHandler(ProductMongoDbContext mongoDbContext, ProductPostgreSqlContext productPostgreSqlContext)
    {
        _mongoDbContext = mongoDbContext;
        _productPostgreSqlContext = productPostgreSqlContext;
    }

    public async Task Handle(CreateProductPhotoCommand request, CancellationToken cancellationToken)
    {
        var productExists = await _productPostgreSqlContext.Set<ProductEntity>().AnyAsync(x => x.Id == request.ProductId, cancellationToken);

        if (!productExists)
            throw new BadRequestException(ExceptionMessage.E007IdIsUnknown);

        var document = new ProductPhotoDocument()
        {
            Data = ConversionHelper.ToByte(request.File),
            Name = request.File.FileName,
            ProductId = request.ProductId,
        };

        await _mongoDbContext.AddAsync(document, cancellationToken);
    }
}