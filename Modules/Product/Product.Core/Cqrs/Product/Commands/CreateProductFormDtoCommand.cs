using MediatR;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Product.Commands;
public record CreateProductFormDtoCommand(ProductFormDto Dto) : IRequest<ProductFormDto>;

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

        return new ProductFormDto(result.Entity);
    }
}