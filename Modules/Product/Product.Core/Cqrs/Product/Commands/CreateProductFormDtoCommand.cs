using MediatR;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.Product.Commands;
public record CreateProductFormDtoCommand(ProductFormDto Dto) : IRequest<ProductFormDto>;

internal class CreateProductFormDtoCommandHandler : IRequestHandler<CreateProductFormDtoCommand, ProductFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public CreateProductFormDtoCommandHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ProductFormDto> Handle(CreateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new ProductFormDto(result.Entity);
    }
}