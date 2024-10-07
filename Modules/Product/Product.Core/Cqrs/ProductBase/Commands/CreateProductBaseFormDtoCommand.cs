using MediatR;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductBase.Commands;

public record CreateProductBaseFormDtoCommand(ProductBaseFormDto Dto) : IRequest<ProductBaseFormDto>;

internal class CreateProductBaseFormDtoCommandHanlder : IRequestHandler<CreateProductBaseFormDtoCommand, ProductBaseFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public CreateProductBaseFormDtoCommandHanlder(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ProductBaseFormDto> Handle(CreateProductBaseFormDtoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Set<ProductBaseEntity>().AddAsync(request.Dto.ToEntity(), cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return new(entity.Entity);
    }
}