using MediatR;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;
using Shared.Core.Bases;

namespace Product.Core.Cqrs.ProductBase.Commands;

public record CreateProductBaseFormDtoCommand(ProductBaseFormDto Dto) : IRequest<ProductBaseFormDto>;

internal class CreateProductBaseFormDtoCommandHanlder : BaseRequestHandler<ProductContext, CreateProductBaseFormDtoCommand, ProductBaseFormDto>
{
    public CreateProductBaseFormDtoCommandHanlder(ProductContext context) : base(context)
    {
    }

    public override async Task<ProductBaseFormDto> Handle(CreateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new ProductBaseFormDto(entity.Entity);
    }
}