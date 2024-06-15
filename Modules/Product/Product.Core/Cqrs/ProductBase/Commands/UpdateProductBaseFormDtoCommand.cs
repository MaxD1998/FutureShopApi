using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.Category;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.ProductBase.Commands;

public record UpdateProductBaseFormDtoCommand(Guid Id, ProductBaseFormDto Dto) : IRequest<ProductBaseFormDto>;

internal class UpdateProductBaseFormDtoCommandHandler : BaseRequestHandler<ProductContext, UpdateProductBaseFormDtoCommand, ProductBaseFormDto>
{
    public UpdateProductBaseFormDtoCommandHandler(ProductContext context) : base(context)
    {
    }

    public override async Task<ProductBaseFormDto> Handle(UpdateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>()
            .Include(x => x.ProductParameters)
            .Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        entity.Update(request.Dto.ToEntity());

        await _context.SaveChangesAsync(cancellationToken);

        return new ProductBaseFormDto(entity);
    }
}