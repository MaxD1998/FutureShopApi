using MediatR;
using Microsoft.EntityFrameworkCore;
using Product.Core.Dtos.ProductBase;
using Product.Domain.Entities;
using Product.Infrastructure;

namespace Product.Core.Cqrs.ProductBase.Queries;
public record GetProductBaseFormDtoByIdQuery(Guid Id) : IRequest<ProductBaseFormDto>;

internal class GetProductBaseFormDtoByIdQueryHandler : IRequestHandler<GetProductBaseFormDtoByIdQuery, ProductBaseFormDto>
{
    private readonly ProductPostgreSqlContext _context;

    public GetProductBaseFormDtoByIdQueryHandler(ProductPostgreSqlContext context)
    {
        _context = context;
    }

    public async Task<ProductBaseFormDto> Handle(GetProductBaseFormDtoByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Set<ProductBaseEntity>()
            .Include(x => x.Products)
            .Include(x => x.ProductParameters)
                .ThenInclude(x => x.Translations)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        return result is null ? null : new ProductBaseFormDto(result);
    }
}