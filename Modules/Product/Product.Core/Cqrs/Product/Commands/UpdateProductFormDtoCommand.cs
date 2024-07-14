using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Product;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.Product.Commands;
public record UpdateProductFormDtoCommand(Guid Id, ProductFormDto Dto) : IRequest<ProductFormDto>;

internal class UpdateProductFormDtoCommandHandler : BaseRequestHandler<ProductContext, UpdateProductFormDtoCommand, ProductFormDto>
{
    public UpdateProductFormDtoCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<ProductFormDto> Handle(UpdateProductFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductEntity>()
            .Include(x => x.ProductParameterValues)
            .Include(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return new ProductFormDto(entity);
    }
}