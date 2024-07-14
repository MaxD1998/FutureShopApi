using MediatR;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Product.Commands;
public record CreateProductFormDtoCommand(ProductFormDto Dto) : IRequest<ProductFormDto>;

internal class CreateProductFormDtoCommandHandler : BaseRequestHandler<ProductContext, CreateProductFormDtoCommand, ProductFormDto>
{
    public CreateProductFormDtoCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<ProductFormDto> Handle(CreateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new ProductFormDto(result.Entity);
    }
}