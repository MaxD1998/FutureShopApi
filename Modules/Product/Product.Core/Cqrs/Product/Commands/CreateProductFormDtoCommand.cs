using MediatR;
using Microsoft.AspNetCore.Http;
using Product.Core.Dtos.Product;
using Product.Core.Extensions;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Product.Commands;
public record CreateProductFormDtoCommand(ProductFormDto Dto, IEnumerable<IFormFile> Files) : IRequest<ProductFormDto>;

internal class CreateProductFormDtoCommandHandler : IRequestHandler<CreateProductFormDtoCommand, ProductFormDto>
{
    private readonly ProductMongoDbContext _mongoDbContext;
    private readonly ProductPostgreSqlContext _postgreSqlContext;

    public CreateProductFormDtoCommandHandler(ProductMongoDbContext mongoDbContext, ProductPostgreSqlContext postgreSqlContext)
    {
        _mongoDbContext = mongoDbContext;
        _postgreSqlContext = postgreSqlContext;
    }

    public async Task<ProductFormDto> Handle(CreateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _postgreSqlContext.Set<ProductEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _postgreSqlContext.SaveChangesAsync(cancellationToken);

        if (result.Entity.ProductPhotos.Count > 0)
        {
            var fileNames = result.Entity.ProductPhotos
                .Select(x => x.Name)
                .ToList();

            var photos = request.Files
                .Where(x => fileNames.Contains(x.FileName))
                .Select(x => x.ToProductPhotoDocument());

            await _mongoDbContext.AddRangeAsync(photos, cancellationToken);
        }

        return new ProductFormDto(result.Entity);
    }
}